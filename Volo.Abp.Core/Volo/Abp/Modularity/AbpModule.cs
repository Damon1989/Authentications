using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Volo.Abp.Modularity
{
    public class AbpModule:
        IAbpModule,
        IOnPreApplicationInitialization,
        IOnApplicationInitialization,
        IOnPostApplicationInitialization,
        IOnApplicationShutdown,
        IPreConfigureServices,
        IPostConfigureServices
    {
        protected internal bool SkipAutoServiceRegistration { get; protected set; }

        protected internal ServiceConfigurationContext ServiceConfigurationContext
        {
            get
            {
                if (_serviceConfigurationContext==null)
                {
                    throw new AbpException($"{nameof(ServiceConfigurationContext)} is only available in the {nameof(ConfigureServices)}," +
                                           $" {nameof(PreConfigureServices)} and {nameof(PostConfigureServices)} methods.");
                }

                return _serviceConfigurationContext;
            }
            internal set => _serviceConfigurationContext = value;
        }

        private ServiceConfigurationContext _serviceConfigurationContext;

        public void PreConfigureServices(ServiceConfigurationContext context)
        {
            throw new NotImplementedException();
        }

        public void ConfigureServices(ServiceConfigurationContext context)
        {
            throw new NotImplementedException();
        }

        public void PostConfigureServices(ServiceConfigurationContext context)
        {
            throw new NotImplementedException();
        }

        public void OnPreApplicationInitialization([NotNull] ApplicationInitializationContext context)
        {
            throw new NotImplementedException();
        }

        public void OnApplicationInitialization([NotNull] ApplicationInitializationContext context)
        {
            throw new NotImplementedException();
        }

        public void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            throw new NotImplementedException();
        }

        public void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            throw new NotImplementedException();
        }

        public static bool IsAbpModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            return typeInfo.IsClass &&
                   !typeInfo.IsAbstract &&
                   !typeInfo.IsGenericType &&
                   typeof(IAbpModule).GetTypeInfo().IsAssignableFrom(type);
        }

        internal static void CheckAbpModuleType(Type moduleType)
        {
            if (!IsAbpModule(moduleType))
            {
                throw new ArgumentException($"Given type is not an ABP module:{moduleType.AssemblyQualifiedName}");
            }
        }

        protected void Configure<TOptions>(Action<TOptions> configureOptions)
            where TOptions:class
        {
            ServiceConfigurationContext.Services.Configure(configureOptions);
        }

        protected void Configure<TOptions>(string name, Action<TOptions> configureOptions)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure(name, configureOptions);
        }

        protected void Configure<TOptions>(IConfiguration configuration)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure<TOptions>(configuration);
        }

        protected void Configure<TOptions>(IConfiguration configuration, Action<BinderOptions> configureBinder)
            where TOptions:class
        {
            ServiceConfigurationContext.Services.Configure<TOptions>(configuration, configureBinder);
        }

        protected void Configure<TOptions>(string name, IConfiguration configuration)
            where TOptions:class
        {
            ServiceConfigurationContext.Services.Configure<TOptions>(name, configuration);
        }

        protected void PreConfigure<TOptions>(Action<TOptions> configureOptions)
            where TOptions:class
        {
        }
    }
}
