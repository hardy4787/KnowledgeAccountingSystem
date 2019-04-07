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
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register([FromBody]RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserDTO userDto = new UserDTO
            {
                Email = model.Email,
                Password = model.Password,
                UserName = model.UserName,
                FullName = model.FullName,
                Role = "user"
            };
            IdentityOperations operationDetails = await UserService.CreateUserAsync(userDto);
            if (!operationDetails.Succeeded)
            {
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                return BadRequest(ModelState);
            }
            return Ok(operationDetails);
        }
        [HttpDelete]
        [Route("{userId}")]
        public async Task<IHttpActionResult> DeleteUser([FromUri] string userId)
        {
            IdentityOperations operationDetails = await UserService.DeleteUser(userId);
            if (operationDetails.Succeeded) return Ok();
            else return BadRequest(operationDetails.Message);
        }
        //private IHttpActionResult GetErrorResult(OperationDetails result)
        //{
        //    if (result == null)
        //    {
        //        return InternalServerError();
        //    }

        //    if (!result.Succedeed)
        //    {
        //        ModelState.AddModelError(result.Property, result.Message);

        //        if (ModelState.IsValid)
        //        {
        //            return BadRequest();
        //        }
        //        return BadRequest(ModelState);
        //    }

        //    return null;
        //}
    }
}
