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

            #region ���нӿڲ�ķ���ע��

            var servicesDllFile = Path.Combine(basePath, "AutofacDemo.Services.dll");
            var repositoryDllFile = Path.Combine(basePath, "AutofacDemo.Repository.dll");

            if (!(File.Exists(servicesDllFile)&&File.Exists(repositoryDllFile)))
            {
                //��Ҫ�޸���Ŀ����Ŀ¼
                var msg = "Repository.dll��service.dll��ʧ����Ϊ��Ŀ�����ˣ�������Ҫ��F6���룬��F5���У�����bin�ļ���,������";
                throw new Exception(msg);
            }


            //��ȡService.dll���򼯷��񣬲�ע��
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                .AsImplementedInterfaces()
                .InstancePerDependency();

            // ��ȡ Repository.dll ���򼯷��񣬲�ע��
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository)
                .AsImplementedInterfaces()
                .InstancePerDependency();

            #endregion
        }
    }
}