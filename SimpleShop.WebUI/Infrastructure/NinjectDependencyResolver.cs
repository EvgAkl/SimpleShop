using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Moq;
using SimpleShop.Domain.Abstract;
using SimpleShop.Domain.Entities;



namespace SimpleShop.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            
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
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game> {
                new Game { Name = "SimCity", Price = 1499M},
                new Game { Name = "TITANFALL", Price = 2299M},
                new Game { Name = "Battlefield 4", Price = 899.4M}
            });

            kernel.Bind<IGameRepository>().ToConstant(mock.Object);
        }






    } // end NinjectDependencyResolver()

} // end namespace