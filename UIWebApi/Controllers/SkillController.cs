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
    public class SkillController : ApiController
    {
        private ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [Route("~/api/skills")]
        public IEnumerable<SkillModel> GetSkills()
        {
            return Mapper.Map<IEnumerable<SkillDTO>, IEnumerable<SkillModel>>(_skillService.GetSkills());
        }

        [Route("{id}/skills")]
        public IEnumerable<ProgrammerSkillModel> Get(string id)
        {
            return Mapper.Map<IEnumerable<ProgrammerSkillDTO>, IEnumerable<ProgrammerSkillModel>>(_skillService.GetSkillsOfProgrammer(id));
        }

        [Route("~/api/profile/{id}/untouched-skills")]
        public IEnumerable<SkillModel> GetUntouchedSkills(string id)
        {
            return Mapper.Map<IEnumerable<SkillDTO>, IEnumerable<SkillModel>>(_skillService.GetSkillsThatTheProgrammerDoesNotHave(id));
        }

        [Route("{id}/skills")]
        public void Post([FromBody]ProgrammerSkillModel programmerSkill)
        {
            _skillService.InsertSkillToProgrammer(Mapper.Map<ProgrammerSkillModel, ProgrammerSkillDTO>(programmerSkill));
        }

        [Route("{id}/skills/{skillId}")]
        public void Put(int skillId, [FromBody]ProgrammerSkillModel programmerSkill)
        {
            if (skillId == programmerSkill.SkillId)
            {
                _skillService.UpdateSkillOfProgrammer(Mapper.Map<ProgrammerSkillModel, ProgrammerSkillDTO>(programmerSkill));
            }
        }

        [Route("{id}/skills/{skillId}")]
        public void Delete(string id, int skillId)
        {
            _skillService.DeleteSkillOfProgrammer(id, skillId);
        }
    }
}
