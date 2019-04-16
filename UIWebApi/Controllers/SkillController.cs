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
using WebApiApp.Filters;

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
            IEnumerable<SkillModel> skills;
            try
            {
                skills = Mapper.Map<IEnumerable<SkillDTO>, IEnumerable<SkillModel>>(_skillService.GetSkills());
            }
            catch (ValidationException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok(skills);
        }
        [ModelValidation]
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateSkill([FromBody]SkillModel skill)
        {
            try
            {
                _skillService.Insert(Mapper.Map<SkillModel, SkillDTO>(skill));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok(new { Message = "Skill created successfully!" });
        }

        [Route("{skillId:int}")]
        public IHttpActionResult DeleteSkill(int skillId)
        {
            try
            {
                _skillService.Delete(skillId);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok(new { Message = "Skill deleted successfully!" });
        }
        [ModelValidation]
        [HttpPut]
        [Route("{skillId:int}")]
        public IHttpActionResult UpdateSkill(int skillId, [FromBody]SkillModel skill)
        {
            try
            {
                _skillService.Update(skillId, Mapper.Map<SkillModel, SkillDTO>(skill));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok(new { message = "Skill changed successfully" });
        }
    }
}
