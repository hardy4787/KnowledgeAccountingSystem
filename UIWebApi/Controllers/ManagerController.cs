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
    [RoutePrefix("api/manager")]
    [Authorize(Roles = "admin")]
    public class ManagerController : ApiController
    {
        private IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [Route("profiles/{skillId}/{knowledgeLevel}")]
        public IEnumerable<ProfileModel> GetProfilesBySkill(int? skillId, int knowledgeLevel)
        {
            return Mapper.Map<IEnumerable<ProgrammerProfileDTO>, IEnumerable<ProfileModel>>(_managerService.GetProgrammersBySkill(skillId, knowledgeLevel));
        }
    }
}
