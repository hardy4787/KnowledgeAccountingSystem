using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SkillService : ISkillService
    {
        private readonly IUnitOfWork Database;
        public SkillService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void DeleteSkillOfProgrammer(string idProgrammer, int idSkill)
        {
            Database.ProgrammerSkills.Delete(idProgrammer, idSkill);
            Database.Save();
        }

        public IEnumerable<ProgrammerSkillDTO> GetSkillsOfProgrammer(string id)
        {
            var programmerSkills = Database.ProgrammerSkills.GetAll().Where(x => x.ProgrammerId == id);
            return Mapper.Map<IEnumerable<ProgrammerSkill>, IEnumerable<ProgrammerSkillDTO>>(programmerSkills);
        }

        public void InsertSkillToProgrammer(ProgrammerSkillDTO skill)
        {
            Database.ProgrammerSkills.Insert(Mapper.Map<ProgrammerSkillDTO, ProgrammerSkill>(skill));
            Database.Save();
        }

        public void UpdateSkillOfProgrammer(ProgrammerSkillDTO skill)
        {
            Database.ProgrammerSkills.Update(Mapper.Map<ProgrammerSkillDTO, ProgrammerSkill>(skill));
            Database.Save();
        }
        public IEnumerable<SkillDTO> GetSkills()
        {
            return Mapper.Map<IEnumerable<Skill>, IEnumerable<SkillDTO>>(Database.Skills.GetAll());
        }
        public IEnumerable<SkillDTO> GetSkillsThatTheProgrammerDoesNotHave(string id)
        {
            var programmerSkills = Database.ProgrammerSkills.GetAll().Where(y => y.ProgrammerId == id).Select(x => x.SkillId).ToList();
            var skills = Database.Skills.GetAll().ToList();
            var skillsIdWhichProgrammerDoesNotHave = skills.Select(x => x.Id).Except(programmerSkills);
            var skillsProgrammerDoesNotHave = skills.Where(x => skillsIdWhichProgrammerDoesNotHave.Contains(x.Id));
            return Mapper.Map<IEnumerable<Skill>, IEnumerable<SkillDTO>>(skillsProgrammerDoesNotHave);
        }
    }
}
