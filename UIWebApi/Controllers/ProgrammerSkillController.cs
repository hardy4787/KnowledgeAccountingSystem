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
    public class ProgrammerSkillController : ApiController
    {
        private readonly ISkillService _skillService;

        public ProgrammerSkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        [Route("{id}/skills")]
        public IHttpActionResult GetProgrammerSkills(string id)
        {
            IEnumerable<ProgrammerSkillModel> skills;
            try
            {
                skills = Mapper.Map<IEnumerable<ProgrammerSkillDTO>, IEnumerable<ProgrammerSkillModel>>(_skillService.GetSkillsOfProgrammer(id));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(skills);
        }

        [HttpGet]
        [Route("{id}/untouched-skills")]
        public IHttpActionResult GetProgrammerUntouchedSkills(string id)
        {
            IEnumerable<SkillModel> skills;
            try
            {
                skills = Mapper.Map<IEnumerable<SkillDTO>, IEnumerable<SkillModel>>(_skillService.GetSkillsThatTheProgrammerDoesNotHave(id));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(skills);
        }

        [HttpPost]
        [Route("{id}/skills")]
        public IHttpActionResult AddSkillToProgrammer([FromBody]ProgrammerSkillModel programmerSkill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _skillService.InsertSkillToProgrammer(Mapper.Map<ProgrammerSkillModel, ProgrammerSkillDTO>(programmerSkill));

            return Ok(new { Message = "Skill added successfully!" });
        }

        [HttpPut]
        [Route("{id}/skills/{skillId}")]
        public IHttpActionResult UpdateSkillProgrammer(int skillId, [FromBody]ProgrammerSkillModel programmerSkill)
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

        [HttpDelete]
        [Route("{id}/skills/{skillId}")]
        public IHttpActionResult DeleteSkillProgrammer(string id, int skillId)
        {
            try
            {
                _skillService.DeleteSkillOfProgrammer(id, skillId);
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
