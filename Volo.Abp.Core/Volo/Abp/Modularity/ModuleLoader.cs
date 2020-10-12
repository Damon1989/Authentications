using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity.PlugIns;

namespace Volo.Abp.Modularity
{
    public class ModuleLoader: IModuleLoader
    {
        public IAbpModuleDescriptor[] LoadModules(
            IServiceCollection services,
            Type startupModuleType,
            PlugInSourceList plugInSources)
        {
            // 验证参数的有效性
            Check.NotNull(services, nameof(services));
            Check.NotNull(startupModuleType, nameof(startupModuleType));
            Check.NotNull(plugInSources, nameof(plugInSources));

            // 扫描模块类型，并构建模块描述对象集合
            var modules = GetDescriptors(services, startupModuleType, plugInSources);

            // 按照模块的依赖性重新排序
            modules = SortByDependency(modules, startupModuleType);

            return modules.ToArray();
        }

        private List<IAbpModuleDescriptor> GetDescriptors(
            IServiceCollection services,
            Type startupModuleType,
            PlugInSourceList plugInSources)
        {
            // 创建一个空的模块描述对象集合
            var modules=new List<AbpModuleDescriptor>();

            // 按照启动模块，递归构建模块描述对象集合
            FillModules(modules,services,startupModuleType,plugInSources);

            // 设置每个模块的依赖项
            SetDependencies(modules);

            // 返回结果
            return modules.Cast<IAbpModuleDescriptor>().ToList();

        }

        protected virtual void FillModules(
            List<AbpModuleDescriptor> modules,
            IServiceCollection services,
            Type startupModuleType,
            PlugInSourceList plugInSources)
        {
            // 调用 AbpModuleHelper 提供的搜索方法
            // All modules starting from the startup module
            foreach (var moduleType in AbpModuleHelper.FindAllModuleTypes(startupModuleType))
            {
                modules.Add(CreateModuleDescriptor(services,moduleType));
            }

            // Plugin modules
            foreach (var moduleType in plugInSources.GetAllModules())
            {
                if (modules.Any(m=>m.Type==moduleType))
                {
                    continue;
                }

                modules.Add(CreateModuleDescriptor(services,moduleType,isLoadedAsPlugIn:true));
            }
        }

        protected virtual void SetDependencies(List<AbpModuleDescriptor> modules)
        {
            // 遍历整个模块描述对象集合
            foreach (var module in modules)
            {
                SetDependencies(modules,module);
            }
        }

        protected virtual List<IAbpModuleDescriptor> SortByDependency(List<IAbpModuleDescriptor> modules,
            Type startupModuleType)
        {
            var sortedModules = modules.SortByDependencies(m => m.Dependencies);
            sortedModules.MoveItem(m=>m.Type==startupModuleType,modules.Count-1);
            return sortedModules;
        }

        protected virtual AbpModuleDescriptor CreateModuleDescriptor(IServiceCollection services, Type moduleType,
            bool isLoadedAsPlugIn = false)
        {
            return new AbpModuleDescriptor(moduleType,CreateAndRegisterModule(services,moduleType),isLoadedAsPlugIn);
        }

        protected virtual IAbpModule CreateAndRegisterModule(IServiceCollection services, Type moduleType)
        {
            var module = (IAbpModule) Activator.CreateInstance(moduleType);
            services.AddSingleton(moduleType, module);
            return module;
        }

        protected virtual void SetDependencies(List<AbpModuleDescriptor> modules, AbpModuleDescriptor module)
        {
            // 根据当前模块描述对象存储的Type类型，获取 DependsOn 标签依赖的类型
            foreach (var dependedModuleType in AbpModuleHelper.FindDependedModuleTypes(module.Type))
            {
                // 在模块描述对象中，按照type类型搜索
                var dependedModule = modules.FirstOrDefault(m => m.Type == dependedModuleType);
                if (dependedModule==null)
                {
                    throw new AbpException($"Could not find a depended module {dependedModuleType.AssemblyQualifiedName} for {module.Type.AssemblyQualifiedName}");
                }

                // 搜索到结果，则添加到当前模块描述对象的Dependencies属性
                module.AddDependency(dependedModule);
            }
        }
    }
}
