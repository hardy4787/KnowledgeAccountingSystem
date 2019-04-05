using BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UIWebApi.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userService = context.OwinContext.GetUserManager<IUserService>();
            var user = await userService.FindUserAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "Invalid username or password.");
                return;
            }
            var userRoles = userService.GetRolesByUserId(user.Id);
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("Id", user.Id));
            foreach (string roleName in userRoles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
            }
            var additionalData = new AuthenticationProperties(new Dictionary<string, string>
            {
                { "Id", user.Id },
                { "role" , Newtonsoft.Json.JsonConvert.SerializeObject(userRoles) }
            });
            //AuthenticationProperties properties = CreateProperties(user.Id);
            
            AuthenticationTicket ticket = new AuthenticationTicket(identity, additionalData);
            context.Validated(ticket);
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }
    }
}