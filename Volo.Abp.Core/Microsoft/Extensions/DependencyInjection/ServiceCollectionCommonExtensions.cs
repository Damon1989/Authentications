using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionCommonExtensions
    {
        public static bool IsAdded<T>(this IServiceCollection services)
        {
            return services.IsAdded(typeof(T));
        }

        public static bool IsAdded(this IServiceCollection services, Type type)
        {
            return services.Any(d => d.ServiceType == type);
        }

        public static T GetSingletonInstanceOrNull<T>(this IServiceCollection services)
        {
            return (T) services
                .FirstOrDefault(d => d.ServiceType == typeof(T))
                ?.ImplementationInstance;
        }

        public static T GetSingletonInstance<T>(this IServiceCollection services)
        {
            var service = services.GetSingletonInstanceOrNull<T>();
            if (service==null)
            {
                throw new InvalidOperationException($"Could not find singleton service: {typeof(T).AssemblyQualifiedName}");
            }

            return service;
        }

        public static IServiceProvider BuildServiceProviderFromFactory([NotNull] this IServiceCollection services)
        {
            Check.NotNull(services, nameof(services));

            foreach (var service in services)
            {
                var factoryInterface = service.ImplementationInstance?.GetType()
                    .GetTypeInfo()
                    .GetInterfaces()
                    .FirstOrDefault(i => i.GetTypeInfo().IsGenericType &&
                                         i.GetGenericTypeDefinition() == typeof(IServiceProviderFactory<>));

                if (factoryInterface==null)
                {
                    continue;
                }

                var containerBuildType = factoryInterface.GenericTypeArguments[0];
                return (IServiceProvider)typeof(ServiceCollectionCommonExtensions)
                    .GetTypeInfo()
                    .GetMethods()
                    .Single(m => m.Name == nameof(BuildServiceProviderFromFactory) && m.IsGenericMethod)
                    .MakeGenericMethod(containerBuildType)
                    .Invoke(null, new[] {services, null});
            }

            return services.BuildServiceProvider();
        }

        public static IServiceProvider BuildServiceProviderFromFactory<TContainerBuilder>(
            [NotNull] this IServiceCollection services, Action<TContainerBuilder> builderAction = null)
        {
            Check.NotNull(services, nameof(services));

            var serviceProviderFactory =
                services.GetSingletonInstanceOrNull<IServiceProviderFactory<TContainerBuilder>>();
            if (serviceProviderFactory==null)
            {
                throw new AbpException($"Could not find {typeof(IServiceProviderFactory<TContainerBuilder>).FullName} in {services}.");
            }

            var builder = serviceProviderFactory.CreateBuilder(services);
            builderAction?.Invoke(builder);
            return serviceProviderFactory.CreateServiceProvider(builder);
        }

        internal static T GetService<T>(this IServiceCollection services)
        {
            return services
                .GetSingletonInstance<IAbpApplication>()
                .ServiceProvider
                .GetService<T>();
        }

        internal static object GetService(this IServiceCollection services, Type type)
        {
            return services
                .GetSingletonInstance<IAbpApplication>()
                .ServiceProvider
                .GetService(type);
        }

        internal static T GetRequiredService<T>(this IServiceCollection services)
        {
            return services
                .GetSingletonInstance<IAbpApplication>()
                .ServiceProvider
                .GetRequiredService<T>();
        }

        internal static object GetRequiredService(this IServiceCollection services, Type type)
        {
            return services
                .GetSingletonInstance<IAbpApplication>()
                .ServiceProvider
                .GetRequiredService(type);
        }

        public static Lazy<T> GetServiceLazy<T>(this IServiceCollection services)
        {
            return new Lazy<T>(services.GetService<T>,true);
        }

        public static Lazy<object> GetServiceLazy(this IServiceCollection services, Type type)
        {
            return new Lazy<object>(()=>services.GetService(type),true);
        }

        public static Lazy<T> GetRequiredServiceLazy<T>(this IServiceCollection services)
        {
            return new Lazy<T>(services.GetRequiredService<T>,true);
        }

        public static Lazy<object> GetRequiredServiceLazy(this IServiceCollection services, Type type)
        {
            return new Lazy<object>(()=>services.GetRequiredService(type),true);
        }
    }
}
