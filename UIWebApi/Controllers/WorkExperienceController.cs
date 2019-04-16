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
using UIWebApi.Filters;
using UIWebApi.Models;
using WebApiApp.Filters;

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

        [Route("{userId}/work-experience")]
        public IHttpActionResult GetByProfileId(string userId)
        {
            IEnumerable<WorkExperienceModel> workExperience;
            try
            {
                workExperience = Mapper.Map<IEnumerable<WorkExperienceDTO>, IEnumerable<WorkExperienceModel>>(_workExperienceService.GetWorkExperienceByProfileId(userId));
            }
            catch (ValidationException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok(workExperience);
        }

        [ModelValidation]
        [AccessActionFilter]
        [HttpPost]
        [Route("{userId}/work-experience")]
        public IHttpActionResult AddWorkExperience(string userId, [FromBody]WorkExperienceModel workExperience)
        {
            try
            {
                _workExperienceService.Insert(Mapper.Map<WorkExperienceModel, WorkExperienceDTO>(workExperience));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok(new { Message = "Work experience added successfully!" });
        }

        [ModelValidation]
        [AccessActionFilter]
        [HttpPut]
        [Route("{userId}/work-experience/{workExperienceId}")]
        public IHttpActionResult UpdateWorkExperience(string userId, int workExperienceId, [FromBody]WorkExperienceModel workExperience)
        {
            try
            {
                _workExperienceService.Update(workExperienceId, Mapper.Map<WorkExperienceModel, WorkExperienceDTO>(workExperience));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok(new { Message = "Work experience updated successfully!" });
        }

        [AccessActionFilter]
        [HttpDelete]
        [Route("{userId}/work-experience/{workExperienceId}")]
        public IHttpActionResult DeleteWorkExperience(string userId, int workExperienceId)
        {
            try
            {
                _workExperienceService.Delete(workExperienceId);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok(new { Message = "Work experience deleted successfully!" });
        }
    }
}
