using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using UIWebApi.Models;
using System.Web;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;
using UIWebApi.Filters;
using WebApiApp.Filters;
using System;

namespace UIWebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        public IUserService UserService
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        [ModelValidation]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register([FromBody]RegisterModel model)
        {
            IdentityOperations operationDetails;
            UserDTO userDto = new UserDTO
            {
                Email = model.Email,
                Password = model.Password,
                UserName = model.UserName,
                FullName = model.FullName,
                Role = "user"
            };
            try
            {
                operationDetails = await UserService.CreateUserAsync(userDto);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            if (!operationDetails.Succeeded)
            {
                return BadRequest(operationDetails.Message);
            }
            return Ok(operationDetails);
        }

        [AccessActionFilter]
        [HttpDelete]
        [Route("{userId}")]
        public async Task<IHttpActionResult> DeleteUser([FromUri] string userId)
        {
            IdentityOperations operationDetails;
            try
            {
                operationDetails = await UserService.DeleteUser(userId);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            if (!operationDetails.Succeeded)
            {
                return BadRequest(operationDetails.Message);
            }
            return Ok(operationDetails);
        }
    }
}
