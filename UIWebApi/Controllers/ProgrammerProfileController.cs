using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using UIWebApi.Filters;
using UIWebApi.Models;

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
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(Mapper.Map<ProgrammerProfileDTO, ProfileModel>(profile));
        }

        [AccessActionFilter]
        [Route("{userId}")]
        [HttpPut]
        public IHttpActionResult UpdateMainInfoProfile(string userId, [FromBody]ProfileModel profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _profileService.Update(userId, Mapper.Map<ProfileModel, ProgrammerProfileDTO>(profile));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
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
                    ModelState.AddModelError(ex.Property, ex.Message);
                    return BadRequest(ModelState);
                }
                postedFile.SaveAs("C:/Users/BogdanHristich/source/repos/KnowledgeAccountingSystem/Angular/src/assets/image-profiles/" + userId + Path.GetExtension(postedFile.FileName));
            }
            else
            {
                return BadRequest("You haven't submitted any files.");
            }
            return Ok();
        }
    }
}
