using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Users;
using DAL.Users;
using IServicesApp;
using Email;
using Imgs;
using Ninject;



namespace MyBlog.Infrastructure
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
            kernel.Bind<IUsersManager>().To<UsersManager>();
            kernel.Bind<IUser>().To<UsersRepository>().InSingletonScope();

            //kernel.Bind<IImgsManager>().To<ImgsManager>().InSingletonScope();

            //kernel.Bind<IEmailManager>().To<EmailManager>().InSingletonScope();
        }
    }
}