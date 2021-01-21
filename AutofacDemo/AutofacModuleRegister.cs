using System;
using System.IO;
using System.Reflection;
using Autofac;

namespace AutofacDemo
{
    public class AutofacModuleRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var basePath = AppContext.BaseDirectory;

            #region 带有接口层的服务注入

            var servicesDllFile = Path.Combine(basePath, "AutofacDemo.Services.dll");
            var repositoryDllFile = Path.Combine(basePath, "AutofacDemo.Repository.dll");

            if (!(File.Exists(servicesDllFile)&&File.Exists(repositoryDllFile)))
            {
                //需要修改项目生成目录
                var msg = "Repository.dll和service.dll丢失，因为项目解耦了，所以需要先F6编译，再F5运行，请检查bin文件夹,并拷贝";
                throw new Exception(msg);
            }


            //获取Service.dll程序集服务，并注册
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                .AsImplementedInterfaces()
                .InstancePerDependency();

            // 获取 Repository.dll 程序集服务，并注册
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository)
                .AsImplementedInterfaces()
                .InstancePerDependency();

            #endregion
        }
    }
}