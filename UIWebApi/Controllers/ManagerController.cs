using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using UIWebApi.Models;

namespace UIWebApi.Controllers
{
    [RoutePrefix("api/manager")]
    [Authorize(Roles = "admin")]
    public class ManagerController : ApiController
    {
        private readonly IProgrammerProfileService _profileService;

        public ManagerController(IProgrammerProfileService profileService)
        {
            _profileService = profileService;
        }

        [Route("profiles/{skillId}/{knowledgeLevel}")]
        public IEnumerable<ProfileModel> GetProfilesBySkill(int? skillId, int knowledgeLevel)
        {
            return Mapper.Map<IEnumerable<ProgrammerProfileDTO>, IEnumerable<ProfileModel>>(_profileService.GetProgrammersBySkill(skillId, knowledgeLevel));
        }
        [Route("profiles/{skillId}/{knowledgeLevel}/create-report")]
        [HttpPost]
        public IHttpActionResult CreateReport(IEnumerable<ProfileModel> profiles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(_profileService.GenerateReport(Mapper.Map<IEnumerable<ProfileModel>, IEnumerable<ProgrammerProfileDTO>>(profiles)))
            };
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Profiles.xlsx"
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            return Ok();
        }

    }
}
