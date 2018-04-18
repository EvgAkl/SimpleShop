using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Ninject;
using SimpleShop.Domain.Abstract;
using SimpleShop.Domain.Entities;
using SimpleShop.Domain.Concrete;
using SimpleShop.WebUI.Infrastructure.Abstract;
using SimpleShop.WebUI.Infrastructure.Concrete;


namespace SimpleShop.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
            
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IGameRepository>().To<EFGameRepository>();

            EmailSetting emailSetting = new EmailSetting()
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSetting);

            kernel.Bind<IAuthProvider>().To<FormAuthProvider>();

        }

    } // end NinjectDependencyResolver()

} // end namespace