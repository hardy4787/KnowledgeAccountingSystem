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
    [RoutePrefix("api/skills")]
    [Authorize(Roles = "admin")]
    public class SkillController : ApiController
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [OverrideFilter]
        [Authorize]
        [Route("")]
        public IHttpActionResult GetSkills()
        {
            var skills = Mapper.Map<IEnumerable<SkillDTO>, IEnumerable<SkillModel>>(_skillService.GetSkills());
            return Ok(skills);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateSkill([FromBody]SkillModel skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _skillService.Insert(Mapper.Map<SkillModel, SkillDTO>(skill));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(new { Message = "Skill created successfully!" });
        }

        [Authorize(Roles = "admin")]
        [Route("{skillId:int}")]
        public IHttpActionResult DeleteSkill(int skillId)
        {
            try
            {
                _skillService.Delete(skillId);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(new { Message = "Skill deleted successfully!" });
        }
        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("{skillId:int}")]
        public IHttpActionResult UpdateSkill(int skillId, [FromBody]SkillModel skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _skillService.Update(skillId, Mapper.Map<SkillModel, SkillDTO>(skill));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(new { message = "Skill changed successfully" });
        }
    }
}
