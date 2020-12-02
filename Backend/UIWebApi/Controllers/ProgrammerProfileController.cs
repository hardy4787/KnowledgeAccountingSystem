using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Http;
using UIWebApi.Filters;
using UIWebApi.Models;
using WebApiApp.Filters;

namespace UIWebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/profile")]
    public class ProgrammerProfileController : ApiController
    {
        private readonly IProgrammerProfileService _profileService;

        public ProgrammerProfileController(IProgrammerProfileService profileService)
        {
            _profileService = profileService;
        }

        [Route("{userId}")]
        public IHttpActionResult GetMainInfoProfile(string userId)
        {
            ProgrammerProfileDTO profile;
            try
            {
                profile = _profileService.Get(userId);
            }
            catch (ValidationException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok(Mapper.Map<ProgrammerProfileDTO, ProfileModel>(profile));
        }
        [ModelValidation]
        [AccessActionFilter]
        [HttpPut]
        [Route("{userId}")]
        public IHttpActionResult UpdateMainInfoProfile(string userId, [FromBody]ProfileModel profile)
        {
            try
            {
                _profileService.Update(userId, Mapper.Map<ProfileModel, ProgrammerProfileDTO>(profile));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok(new { Message = "Profile edit successfully!" });
        }

        [AccessActionFilter]
        [HttpPut]
        [Route("{userId}/imageProfile")]
        public IHttpActionResult UploadImageProfile(string userId)
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var postedFile = httpRequest.Files["Image"];
                try
                {
                    _profileService.UpdateImageProfileUrl("/assets/image-profiles/", Path.GetExtension(postedFile.FileName), postedFile.ContentLength, userId);
                }
                catch (ValidationException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (Exception)
                {
                    return InternalServerError();
                }
                postedFile.SaveAs("C:/Users/Bohdan_Khrystych/Desktop/New-folder/KnowledgeAccountingSystem/Angular/src/assets/image-profiles/" + userId + Path.GetExtension(postedFile.FileName));
            }
            else
            {
                return BadRequest("You haven't submitted any files.");
            }
            return Ok();
        }
    }
}
