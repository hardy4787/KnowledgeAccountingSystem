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
    public class ProgrammerSkillController : ApiController
    {
        private readonly ISkillService _skillService;

        public ProgrammerSkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        [Route("{userId}/skills")]
        public IHttpActionResult GetProgrammerSkills(string userId)
        {
            IEnumerable<ProgrammerSkillModel> skills;
            try
            {
                skills = Mapper.Map<IEnumerable<ProgrammerSkillDTO>, IEnumerable<ProgrammerSkillModel>>(_skillService.GetSkillsOfProgrammer(userId));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }

            return Ok(skills);
        }

        [HttpGet]
        [Route("{userId}/untouched-skills")]
        public IHttpActionResult GetProgrammerUntouchedSkills(string userId)
        {
            IEnumerable<SkillModel> skills;
            try
            {
                skills = Mapper.Map<IEnumerable<SkillDTO>, IEnumerable<SkillModel>>(_skillService.GetSkillsThatTheProgrammerDoesNotHave(userId));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(skills);
        }

        [AccessActionFilter]
        [HttpPost]
        [Route("{userId}/skills")]
        public IHttpActionResult AddSkillToProgrammer(string userId, [FromBody]ProgrammerSkillModel programmerSkill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _skillService.InsertSkillToProgrammer(Mapper.Map<ProgrammerSkillModel, ProgrammerSkillDTO>(programmerSkill));

            return Ok(new { Message = "Skill added successfully!" });
        }

        [AccessActionFilter]
        [HttpPut]
        [Route("{userId}/skills/{skillId}")]
        public IHttpActionResult UpdateSkillProgrammer(string userId, int skillId, [FromBody]ProgrammerSkillModel programmerSkill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _skillService.UpdateSkillOfProgrammer(skillId, Mapper.Map<ProgrammerSkillModel, ProgrammerSkillDTO>(programmerSkill));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(new { Message = "Skill updated successfully!" });
        }

        [AccessActionFilter]
        [HttpDelete]
        [Route("{userId}/skills/{skillId}")]
        public IHttpActionResult DeleteSkillProgrammer(string userId, int skillId)
        {
            try
            {
                _skillService.DeleteSkillOfProgrammer(userId, skillId);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(new { Message = "Skill deleted successfully!" });
        }
    }
}
