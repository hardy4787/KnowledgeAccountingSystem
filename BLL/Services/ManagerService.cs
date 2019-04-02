using BLL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using DAL.Interfaces;
using AutoMapper;
using BLL.DTO;

namespace BLL.Services
{
    public class ManagerService : IManagerService
    {
        IUnitOfWork Database { get; set; }
        public ManagerService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public IEnumerable<ProgrammerProfileDTO> GetAllProgramers()
        {
            var profiles = Database.ProgrammerProfiles.GetAll();
            return Mapper.Map<IEnumerable<ProgrammerProfile>, IEnumerable<ProgrammerProfileDTO>>(profiles);
        }

        public IEnumerable<ProgrammerProfileDTO> GetProgrammersBySkill(int? idSkill, int knowledgeLevel)
        {
            IEnumerable<string> profilesId = new List<string>();
            if (idSkill == null)
                profilesId = Database.ProgrammerSkills.GetAll().Where(x => x.KnowledgeLevel >= knowledgeLevel).Select(y => y.ProgrammerId).ToList();
            else
                profilesId = Database.ProgrammerSkills.GetAll().Where(x => x.SkillId == idSkill && x.KnowledgeLevel >= knowledgeLevel).Select(y => y.ProgrammerId).ToList();
            IEnumerable<ProgrammerProfile> profiles = Database.ProgrammerProfiles.GetAll().Where(x => profilesId.Contains(x.Id));
            return Mapper.Map<IEnumerable<ProgrammerProfile>, IEnumerable<ProgrammerProfileDTO>>(profiles);
        }
    }
}
