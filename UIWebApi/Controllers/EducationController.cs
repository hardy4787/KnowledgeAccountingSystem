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
        [Route("{id}/education")]
        public IHttpActionResult GetByProfileId(string id)
        {
            IEnumerable<EducationModel> education;
            try
            {
                education = Mapper.Map<IEnumerable<EducationDTO>, IEnumerable<EducationModel>>(_educationService.GetEducationByProfileId(id));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(education);
        }

        [HttpPost]
        [Route("{id}/education")]
        public IHttpActionResult AddEducationProgrammer([FromBody]EducationModel education)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _educationService.Insert(Mapper.Map<EducationModel, EducationDTO>(education));
            return Ok(new { Message = "Education added successfully!" });
        }

        [Route("{id}/education/{educationId}")]
        [HttpPut]
        public IHttpActionResult UpdateEducationProgrammer(int educationId, [FromBody]EducationModel education)
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

        [HttpDelete]
        [Route("{id}/education/{educationId}")]
        public IHttpActionResult Delete(int educationId)
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

