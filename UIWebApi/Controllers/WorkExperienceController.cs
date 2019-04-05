using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UIWebApi.Models;

namespace UIWebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/profile")]
    public class WorkExperienceController : ApiController
    {
        private IWorkExperienceService _workExperienceService;

        public WorkExperienceController(IWorkExperienceService workExperienceService)
        {
            _workExperienceService = workExperienceService;
        }

        [Route("{id}/work-experience")]
        public IEnumerable<WorkExperienceModel> Get(string id)
        {
            return Mapper.Map<IEnumerable<WorkExperienceDTO>, IEnumerable<WorkExperienceModel>>(_workExperienceService.GetWorkExperienceByProfileId(id));
        }

        [Route("{id}/work-experience")]
        public void Post([FromBody]WorkExperienceModel workExperience)
        {
            _workExperienceService.Insert(Mapper.Map<WorkExperienceModel, WorkExperienceDTO>(workExperience));
        }

        [Route("{id}/work-experience/{workExperienceId}")]
        public void Put(int workExperienceId, [FromBody]WorkExperienceModel workExperience)
        {
            if (workExperienceId == workExperience.Id)
            {
                _workExperienceService.Update(Mapper.Map<WorkExperienceModel, WorkExperienceDTO>(workExperience));
            }
        }


        [Route("{id}/work-experience/{workExperienceId}")]
        public void Delete(int workExperienceId)
        {
            _workExperienceService.Delete(workExperienceId);
        }
    }
}
