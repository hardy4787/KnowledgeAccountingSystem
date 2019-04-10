using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
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

        [Route("profiles")]
        public IEnumerable<ProfileModel> GetProfilesBySkill(int? skillId, int knowledgeLevel)
        {
            return Mapper.Map<IEnumerable<ProgrammerProfileDTO>, IEnumerable<ProfileModel>>(_profileService.GetProgrammersBySkill(skillId, knowledgeLevel));
        }

        [Route("profiles/{skillId}/{knowledgeLevel}/create-report")]
        [HttpPost]
        public HttpResponseMessage CreateReport(IEnumerable<ProfileModel> profiles)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                response.Content = new ByteArrayContent(_profileService.GenerateReport(Mapper.Map<IEnumerable<ProfileModel>, IEnumerable<ProgrammerProfileDTO>>(profiles)));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Profiles.xlsx"
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            return response;
        }
    }
}
