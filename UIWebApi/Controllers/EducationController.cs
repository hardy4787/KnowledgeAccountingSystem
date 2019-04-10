using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UIWebApi.Filters;
using UIWebApi.Models;

namespace UIWebApi.Controllers
{

    [Authorize]
    [RoutePrefix("api/profile")]
    public class EducationController : ApiController
    {
        private readonly IEducationService _educationService;

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        [HttpGet]
        [Route("{userId}/education")]
        public IHttpActionResult GetByProfileId(string userId)
        {
            IEnumerable<EducationModel> education;
            try
            {
                education = Mapper.Map<IEnumerable<EducationDTO>, IEnumerable<EducationModel>>(_educationService.GetEducationByProfileId(userId));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(education);
        }

        [AccessActionFilter]
        [HttpPost]
        [Route("{userId}/education")]
        public IHttpActionResult AddEducationProgrammer(string userId, [FromBody]EducationModel education)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _educationService.Insert(Mapper.Map<EducationModel, EducationDTO>(education));
            return Ok(new { Message = "Education added successfully!" });
        }

        [AccessActionFilter]
        [Route("{userId}/education/{educationId}")]
        [HttpPut]
        public IHttpActionResult UpdateEducationProgrammer(string userId, int educationId, [FromBody]EducationModel education)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _educationService.Update(educationId, Mapper.Map<EducationModel, EducationDTO>(education));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(new { Message = "Education updated successfully!" });
        }

        [AccessActionFilter]
        [HttpDelete]
        [Route("{userId}/education/{educationId}")]
        public IHttpActionResult Delete(string userId, int educationId)
        {
            try
            {
                _educationService.Delete(educationId);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(new { Message = "Education deleted successfully!" });
        }
    }
}

