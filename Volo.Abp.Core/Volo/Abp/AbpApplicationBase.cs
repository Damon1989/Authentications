using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Core.Volo.Abp;
using Volo.Abp.Internal;
using Volo.Abp.Modularity;

namespace Volo.Abp
{
    public abstract class AbpApplicationBase:IAbpApplication
    {
        public IReadOnlyList<IAbpModuleDescriptor> Modules { get; }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        [NotNull]
        public Type StartupModuleType { get; }
        public IServiceCollection Services { get; }
        public IServiceProvider ServiceProvider { get; }

        public AbpApplicationBase(
            [NotNull]Type startupModuleType,
            [NotNull]IServiceCollection services,
            [CanBeNull] Action<AbpApplicationCreationOptions> optionsAction)
        {
            Check.NotNull(startupModuleType, nameof(startupModuleType));
            Check.NotNull(services, nameof(services));

            // 配置当前系统的启动模块，以便按照依赖关系进行查找。
            StartupModuleType = startupModuleType;
            Services = services;

            services.TryAddObjectAccessor<IServiceProvider>();
            var options=new AbpApplicationCreationOptions(services);
            optionsAction?.Invoke(options);

            // 当前的Application 就是一个模块容器
            services.AddSingleton<IAbpApplication>(this);
            services.AddSingleton<IModuleContainer>(this);

            services.AddCoreServices();
            // 注入模块加载类，以及模块的四个应用程序生命周期
            services.AddCoreAbpServices(this,options);

            // 遍历所有模块，并按照预加载、初始化、初始化完成的顺序执行其生命周期方法
            Modules = LoadModules(services, options);
        }

        protected virtual void ConfigureServices()
        {
            // 构造一个服务上下文，并将其添加到IoC容器当中。
            var context=new ServiceConfigurationContext(Services);

            Services.AddSingleton(context);

            foreach (var module in Modules)
            {
                if (module.Instance is AbpModule abpModule)
                {
                    abpModule.ServiceConfigurationContext = context;
                }
            }

            //PreConfigureServices
            // 执行预加载方法 PreConfigureServices.
            foreach (var module in Modules.Where(m=>m.Instance is IPreConfigureServices))
            {
                try
                {
                    ((IPreConfigureServices)module.Instance).PreConfigureServices(context);
                }
                catch (Exception ex)
                {
                    throw new AbpInitializationException($"An error occurred during {nameof(IPreConfigureServices.PreConfigureServices)} phase of the module " +
                                                         $" {module.Type.AssemblyQualifiedName}. See the inner exception for details.",ex);
                }
            }

            //ConfigureServices
            // 执行初始化方法 ConfigureServices
            foreach (var module in Modules)
            {
                if (module.Instance is AbpModule abpModule)
                {
                    if (!abpModule.SkipAutoServiceRegistration)
                    {
                        Services.AddAssembly(module.Type.Assembly);
                    }
                }

                try
                {
                    module.Instance.ConfigureServices(context);
                }
                catch (Exception ex)
                {
                    throw new AbpInitializationException($"An error occurred during {nameof(IAbpModule.ConfigureServices)} phase of the module" +
                                                         $" {module.Type.AssemblyQualifiedName}.See the inner exception for details ",ex);
                }
            }

            //PostConfigureServices
            // 执行初始化完成方法 PostConfigureServices.
            foreach (var module in Modules.Where(m=>m.Instance is IPostConfigureServices))
            {
                try
                {
                    ((IPostConfigureServices)module.Instance).PostConfigureServices(context);
                }
                catch (Exception ex)
                {
                    throw new AbpInitializationException($"An error occurred during {nameof(IPostConfigureServices.PostConfigureServices)} phase" +
                                                         $" of the module {module.Type.AssemblyQualifiedName}.See the inner exception for details.",ex);
                }
            }

            // 将服务上下文置为 NULL
            foreach (var module in Modules)
            {
                if (module.Instance is AbpModule abpModule)
                {
                    abpModule.ServiceConfigurationContext = null;
                }
            }
        }

        protected virtual IReadOnlyList<IAbpModuleDescriptor> LoadModules(IServiceCollection services,
            AbpApplicationCreationOptions options)
        {
            // 从IOC容器当中得到模块加载器
            return services
                .GetSingletonInstance<IModuleLoader>()
                .LoadModules(
                    services,
                    StartupModuleType,
                    options.PlugInSources);
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }
    }
}
