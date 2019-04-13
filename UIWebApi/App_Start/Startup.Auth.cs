using BLL.Interfaces;
using BLL.Services;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using Microsoft.Owin;
using UIWebApi.Providers;

namespace UIWebApi
{
    public partial class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(CreateUserService);

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
            app.UseOAuthBearerTokens(OAuthOptions);
        }
        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("DefaultConnection");
        }

    }
}