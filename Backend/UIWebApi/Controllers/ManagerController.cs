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
using WebApiApp.Filters;

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
        public IHttpActionResult GetProfilesBySkill(int? skillId, int knowledgeLevel)
        {
            IEnumerable<ProfileModel> profiles;
            try
            {
                profiles =  Mapper.Map<IEnumerable<ProgrammerProfileDTO>, IEnumerable<ProfileModel>>(_profileService.GetProgrammersBySkill(skillId, knowledgeLevel));
            }
            catch (ValidationException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok(profiles);
        }

        [ModelValidation]
        [Route("profiles/{skillId}/{knowledgeLevel}/create-report")]
        [HttpPost]
        public HttpResponseMessage CreateReport(List<ProfileModel> profiles)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                response.Content = new ByteArrayContent(_profileService.GenerateReport(Mapper.Map<List<ProfileModel>, List<ProgrammerProfileDTO>>(profiles)));
            }
            catch (ValidationException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
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
