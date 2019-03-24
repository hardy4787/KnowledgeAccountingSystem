using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(UI_WebApi_.Startup))]

namespace UI_WebApi_
{
    public partial class Startup
    {
        HttpConfiguration config = new HttpConfiguration();
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);

            //WebApiConfig.Register(config);
            //app.UseWebApi(config);
        }

    }
}
