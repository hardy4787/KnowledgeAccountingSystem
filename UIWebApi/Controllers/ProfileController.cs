using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using UIWebApi.Models;

namespace UIWebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/profile")]
    public class ProfileController : ApiController
    {
        private IProgrammerProfileService _profileService;

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
        public void Put(string id, [FromBody]ProfileModel profile)
        {
            if (id == profile.Id)
            {
                _profileService.Update(Mapper.Map<ProfileModel, ProgrammerProfileDTO>(profile));
            }
        }

        [HttpPut]
        [Route("{id}/imageProfile")]
        public HttpResponseMessage UploadImage(string id)
        {
            var httpRequest = HttpContext.Current.Request;
            HttpPostedFile httpPostedFile = httpRequest.Files["Image"];
            httpPostedFile.SaveAs("C:/Users/BogdanHristich/source/repos/KnowledgeAccountingSystem/Angular/src/assets/image-profiles/" + id + Path.GetExtension(httpPostedFile.FileName));
            _profileService.UpdateImageProfileUrl("/assets/image-profiles/" + id + Path.GetExtension(httpPostedFile.FileName), id); //??
            return Request.CreateResponse(HttpStatusCode.Created);
        }

    }
}
