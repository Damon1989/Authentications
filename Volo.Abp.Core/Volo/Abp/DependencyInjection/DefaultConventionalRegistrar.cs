using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Volo.Abp.DependencyInjection
{
    public class DefaultConventionalRegistrar:ConventionalRegistrarBase
    {
        public override void AddType(IServiceCollection services, Type type)
        {
            // 判断类型是否标注了 DisableConventionalRegistration 特性，如果有标注，则跳过
            if (IsConventionalRegistrationDisabled(type))
            {
                return;
            }

            // 获取 Dependency 特性，如果没有则返回 null
            var dependencyAttribute = GetDependencyAttributeOrNull(type);
            // 优先使用 Dependency 特性所指定的生命周期，如果不存在则根据 type 实现的接口确定生命周期
            var lifeTime = GetLifeTimeOrNull(type, dependencyAttribute);

            if (lifeTime==null)
            {
                return;
            }

            // 获得等待注册的类型定义，类型的定义优先使用 ExposeServices 特性指定的类型，如果没有则使用
            // 类型当中接口以I 开始,后面为实现类型名称的接口
            var exposedServiceTypes = ExposedServiceExplorer.GetExposedServices(type);

            TriggerServiceExposing(services,type,exposedServiceTypes);

            foreach (var exposedServiceType in exposedServiceTypes)
            {
                var serviceDescriptor = CreateServiceDescriptor(
                    type,
                    exposedServiceType,
                    exposedServiceTypes,
                    lifeTime.Value
                );
                if (dependencyAttribute?.ReplaceServices==true)
                {
                    // 替换服务
                    services.Replace(serviceDescriptor);
                } else if (dependencyAttribute?.TryRegister == true)
                {
                    // 注册服务
                    services.TryAdd(serviceDescriptor);
                }
                else
                {
                    // 注册服务
                    services.Add(serviceDescriptor);
                }

            }
        }

        protected virtual ServiceDescriptor CreateServiceDescriptor(
            Type implementationType,
            Type exposingServiceType,
            List<Type> allExposingServiceTypes,
            ServiceLifetime lifetime
        )
        {
            if (lifetime.IsIn(ServiceLifetime.Singleton,ServiceLifetime.Scoped))
            {
                var redirectedType = GetRedirectedTypeOrNull(
                    implementationType,
                    exposingServiceType,
                    allExposingServiceTypes
                );
                if (redirectedType!=null)
                {
                    return ServiceDescriptor.Describe(
                        exposingServiceType,
                        provider => provider.GetService(redirectedType),
                        lifetime
                    );
                }
            }

            return ServiceDescriptor.Describe(
                exposingServiceType,
                implementationType,
                lifetime
            );
        }

        protected virtual Type GetRedirectedTypeOrNull(
            Type implementationType,
            Type exposingServiceType,
            List<Type> allExposingServiceTypes)
        {
            if (allExposingServiceTypes.Count<2)
            {
                return null;
            }

            if (exposingServiceType==implementationType)
            {
                return null;
            }

            if (allExposingServiceTypes.Contains(implementationType))
            {
                return implementationType;
            }

            return allExposingServiceTypes.FirstOrDefault(
                t => t != exposingServiceType && exposingServiceType.IsAssignableFrom(t));
        }

        protected virtual DependencyAttribute GetDependencyAttributeOrNull(Type type)
        {
            return type.GetCustomAttribute<DependencyAttribute>(true);
        }

        protected virtual ServiceLifetime? GetLifeTimeOrNull(Type type,
            [CanBeNull] DependencyAttribute dependencyAttribute)
        {
            return dependencyAttribute?.Lifetime ?? GetServiceLifetimeFromClassHierarcy(type);
        }

        protected virtual ServiceLifetime? GetServiceLifetimeFromClassHierarcy(Type type)
        {
            if (typeof(ITransientDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Transient;
            }

            if (typeof(ISingletonDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Singleton;
            }

            if (typeof(IScopedDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Scoped;
            }

            return null;
        }
    }
}
