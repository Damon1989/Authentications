using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp.Modularity
{
    internal static class AbpModuleHelper
    {
        public static List<Type> FindAllModuleTypes(Type startupModuleType)
        {
            var moduleTypes=new List<Type>();
            // 递归构建模块类型集合
            AddModuleAndDependenciesResursively(moduleTypes,startupModuleType);
            return moduleTypes;
        }

        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            AbpModule.CheckAbpModuleType(moduleType);

            var dependencies=new List<Type>();

            // 从传入的类型当中，获取DependsOn特性
            var dependencyDescriptors = moduleType
                .GetCustomAttributes()
                .OfType<IDependedTypesProvider>();

            // 可能有多个特性标签，遍历
            foreach (var descriptor in dependencyDescriptors)
            {
                foreach (var dependedModuleType in descriptor.GetDependedTypes())
                {
                    dependencies.AddIfNotContains(dependedModuleType);
                }
            }

            return dependencies;
        }

        private static void AddModuleAndDependenciesResursively(List<Type> moduleTypes, Type moduleType)
        {
            // 检查传入的类型是否是模块类
            AbpModule.CheckAbpModuleType(moduleType);

            // 集合已经包含了类型定义，则返回
            if (moduleTypes.Contains(moduleType))
            {
                return;
            }

            moduleTypes.Add(moduleType);

            // 遍历其 DependsOn 特性定义的类型，递归将其类型添加到集合当中
            foreach (var dependedModuleType in FindDependedModuleTypes(moduleType))
            {
                AddModuleAndDependenciesResursively(moduleTypes,dependedModuleType);
            }
        }
    }
}
