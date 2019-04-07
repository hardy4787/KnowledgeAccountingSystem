using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using UIWebApi.Models;

namespace UIWebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/profile")]
    public class ProfileController : ApiController
    {
        private readonly IProgrammerProfileService _profileService;

        public ProfileController(IProgrammerProfileService profileService)
        {
            _profileService = profileService;
        }

        [Route("{id}")]
        public ProfileModel Get(string id)
        {
            return Mapper.Map<ProgrammerProfileDTO, ProfileModel>(_profileService.Get(id));
        }

        [Route("{id}")]
        public IHttpActionResult Put(string id, [FromBody]ProfileModel profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             _profileService.Update(Mapper.Map<ProfileModel, ProgrammerProfileDTO>(profile));

            return Ok();
        }

        [HttpPut]
        [Route("{id}/imageProfile")]
        public IHttpActionResult UploadImage(string id)
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var postedFile = httpRequest.Files["Image"];
                postedFile.SaveAs("C:/Users/BogdanHristich/source/repos/KnowledgeAccountingSystem/Angular/src/assets/image-profiles/" + id + Path.GetExtension(postedFile.FileName));
                _profileService.UpdateImageProfileUrl("/assets/image-profiles/" + id + Path.GetExtension(postedFile.FileName), id);
            }
            else
            {
                return BadRequest("You have not submitted any files.");
            }
            return Ok();
        }

    }
}
