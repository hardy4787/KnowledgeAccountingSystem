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
        public async Task<IHttpActionResult> Register(RegisterModel model)
        {
            //await SetInitialDataAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserDTO userDto = new UserDTO
            {
                Email = model.Email,
                Password = model.Password,
                UserName = model.UserName,
                Role = "user"
            };
            OperationDetails operationDetails = await UserService.Create(userDto);
            if (!operationDetails.Succedeed)
                return GetErrorResult(operationDetails);
            return Ok();
        }
        private IHttpActionResult GetErrorResult(OperationDetails result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succedeed)
            {
                ModelState.AddModelError(result.Property, result.Message);

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }
                return BadRequest(ModelState);
            }

            return null;
        }
        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "somemail@mail.ru",
                UserName = "somemail@mail.ru",
                Password = "ad46D_ewr3",
                FullName = "Семен Семенович Горбунков",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }
    }
}
