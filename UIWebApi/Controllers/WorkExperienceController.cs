using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using UIWebApi.Models;

namespace UIWebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/profile")]
    public class WorkExperienceController : ApiController
    {
        private readonly IWorkExperienceService _workExperienceService;

        public WorkExperienceController(IWorkExperienceService workExperienceService)
        {
            _workExperienceService = workExperienceService;
        }

        [Route("{id}/work-experience")]
        public IHttpActionResult GetByProfileId(string id)
        {
            IEnumerable<WorkExperienceModel> workExperience;
            try
            {
                workExperience = Mapper.Map<IEnumerable<WorkExperienceDTO>, IEnumerable<WorkExperienceModel>>(_workExperienceService.GetWorkExperienceByProfileId(id));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(workExperience);
        }

        [Route("{id}/work-experience")]
        [HttpPost]
        public IHttpActionResult AddWorkExperience(string id, [FromBody]WorkExperienceModel workExperience)
        {
            //var identityClaims = (ClaimsIdentity)User.Identity;
            //string idSession = identityClaims.FindFirst("Id").Value;
            //if (id != idSession)
            //    return StatusCode(HttpStatusCode.Forbidden);
            _workExperienceService.Insert(Mapper.Map<WorkExperienceModel, WorkExperienceDTO>(workExperience));
            return Ok(new { Message = "Work experience added successfully!" });
        }
        [HttpPut]
        [Route("{id}/work-experience/{workExperienceId}")]
        public IHttpActionResult UpdateWorkExperience(int workExperienceId, [FromBody]WorkExperienceModel workExperience)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _workExperienceService.Update(workExperienceId, Mapper.Map<WorkExperienceModel, WorkExperienceDTO>(workExperience));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(new { Message = "Work experience updated successfully!" });
        }


        [HttpDelete]
        [Route("{id}/work-experience/{workExperienceId}")]
        public IHttpActionResult DeleteWorkExperience(int workExperienceId)
        {
            try
            {
                _workExperienceService.Delete(workExperienceId);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(new { Message = "Work experience deleted successfully!" });
        }
    }
}
