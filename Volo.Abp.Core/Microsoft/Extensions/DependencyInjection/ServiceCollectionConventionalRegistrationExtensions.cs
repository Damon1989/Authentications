using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionConventionalRegistrationExtensions
    {
        public static IServiceCollection AddConventionalRegister(this IServiceCollection services,
            IConventionalRegistrar registrar)
        {
            GetOrCreateRegistrarList(services).Add(registrar);
            return services;
        }

        public static List<IConventionalRegistrar> GetConventionalRegistrars(this IServiceCollection services)
        {
            return GetOrCreateRegistrarList(services);
        }

        private static ConventionalRegistrarList GetOrCreateRegistrarList(IServiceCollection services)
        {
            var conventionalRegistrars =
                services.GetSingletonInstanceOrNull<IObjectAccessor<ConventionalRegistrarList>>()?.Value;
            if (conventionalRegistrars==null)
            {
                conventionalRegistrars=new ConventionalRegistrarList();
            }

            return conventionalRegistrars;
        }

        public static IServiceCollection AddAssemblyOf<T>(this IServiceCollection services)
        {
            return services.AddAssembly(typeof(T).GetTypeInfo().Assembly);
        }

        public static IServiceCollection AddAssembly(this IServiceCollection services, Assembly assembly)
        {
            // 获得所有规约注册器，然后调用规约注册器的AddAssembly方法注册类型。
            foreach (var registrar in services.GetConventionalRegistrars())
            {
                registrar.AddAssembly(services,assembly);
            }

            return services;
        }

        public static IServiceCollection AddTypes(this IServiceCollection services, params Type[] types)
        {
            foreach (var registrar in services.GetConventionalRegistrars())
            {
                registrar.AddTypes(services,types);
            }

            return services;
        }

        public static IServiceCollection AddType<IType>(this IServiceCollection services)
        {
            return services.AddType(typeof(IType));
        }

        public static IServiceCollection AddType(this IServiceCollection services, Type type)
        {
            foreach (var registrar in services.GetConventionalRegistrars())
            {
                registrar.AddType(services,type);
            }

            return services;
        }
    }
}
