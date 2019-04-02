using Ninject;
using Ninject.Modules;
using System.Web.Http;
using Util;
using Util.Ninject;

namespace UIWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<ApplicationProfile>());

            GlobalConfiguration.Configure(WebApiConfig.Register);

            // внедрение зависимостей
            NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            var ninjectResolver = new NinjectDependencyResolver(kernel);
            GlobalConfiguration.Configuration.DependencyResolver = ninjectResolver;
        }
    }
}
