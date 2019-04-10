using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace UIWebApi.Filters
{
    public class AccessActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            if(filterContext.ActionArguments["userId"].ToString() != claimsIdentity.FindFirst("Id").Value)
                filterContext.Response = filterContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.Forbidden, "You do not have access to this action");
            base.OnActionExecuting(filterContext);
        }
    }
}