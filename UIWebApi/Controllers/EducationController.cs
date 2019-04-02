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
    public class EducationController : ApiController
    {
        private IEducationService _educationService;

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        [Route("{id}/education")]
        public IEnumerable<EducationModel> Get(string id)
        {
            return Mapper.Map<IEnumerable<EducationDTO>, IEnumerable<EducationModel>>(_educationService.GetEducationByProfileId(id));
        }

        [Route("{id}/education")]
        public void Post([FromBody]EducationModel education)
        {
            _educationService.Insert(Mapper.Map<EducationModel, EducationDTO>(education));
        }

        [Route("{id}/education/{educationId}")]
        public void Put(int educationId, [FromBody]EducationModel education)
        {
            if (educationId == education.Id)
            {
                _educationService.Update(Mapper.Map<EducationModel, EducationDTO>(education));
            }
        }


        [Route("{id}/education/{educationId}")]
        public void Delete(int educationId)
        {
            _educationService.Delete(educationId);
        }
    }
}

