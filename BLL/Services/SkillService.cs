using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
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
            var programmer = Database.ProgrammerProfiles.Get(idProgrammer);
            if (programmer == null)
                throw new ValidationException("Programmer has not found", "Id");
            var skill = Database.ProgrammerSkills.Get(idProgrammer, idSkill);
            if (skill == null)
                throw new ValidationException("Programmer does't have this skill", "Id");
            Database.ProgrammerSkills.Delete(idProgrammer, idSkill);
            Database.Save();
        }

        public IEnumerable<ProgrammerSkillDTO> GetSkillsOfProgrammer(string id)
        {
            var programmer = Database.ProgrammerProfiles.Get(id);
            if (programmer == null)
                throw new ValidationException("Programmer has not found", "Id");
            var programmerSkills = Database.ProgrammerSkills.GetAll().Where(x => x.ProgrammerId == id);
            return Mapper.Map<IEnumerable<ProgrammerSkill>, IEnumerable<ProgrammerSkillDTO>>(programmerSkills);
        }

        public void InsertSkillToProgrammer(ProgrammerSkillDTO skillDTO)
        {
            var skill = Database.ProgrammerSkills.Get(skillDTO.ProgrammerId, skillDTO.SkillId);
            if (skill != null)
                throw new ValidationException("Skill of programmer with this id already exists", "Id");
            Database.ProgrammerSkills.Insert(Mapper.Map<ProgrammerSkillDTO, ProgrammerSkill>(skillDTO));
            Database.Save();
        }

        public void UpdateSkillOfProgrammer(int skillId, ProgrammerSkillDTO skillDTO)
        {
            if(skillId != skillDTO.SkillId)
                throw new ValidationException("Skill's id don't match", "Id");
            var skill = Database.ProgrammerSkills.Get(skillDTO.ProgrammerId, skillId);
            if(skill == null)
                throw new ValidationException("Programmer does't have this skill", "Id");
            Database.ProgrammerSkills.Update(Mapper.Map<ProgrammerSkillDTO, ProgrammerSkill>(skillDTO));
            Database.Save();
        }
        public IEnumerable<SkillDTO> GetSkills()
        {
            var skills = Database.Skills.GetAll();
            return Mapper.Map<IEnumerable<Skill>, IEnumerable<SkillDTO>>(skills);
        }
        public IEnumerable<SkillDTO> GetSkillsThatTheProgrammerDoesNotHave(string id)
        {
            var programmer = Database.ProgrammerProfiles.Get(id);
            if (programmer == null)
                throw new ValidationException("Programmer has not found", "Id");
            var programmerSkills = Database.ProgrammerSkills.GetAll().Where(y => y.ProgrammerId == id).Select(x => x.SkillId).ToList();
            if(programmerSkills.Count() == 0)
                return Mapper.Map<IEnumerable<Skill>, IEnumerable<SkillDTO>>(Database.Skills.GetAll());
            var skills = Database.Skills.GetAll().ToList();
            var skillsIdWhichProgrammerDoesNotHave = skills.Select(x => x.Id).Except(programmerSkills);
            var skillsProgrammerDoesNotHave = skills.Where(x => skillsIdWhichProgrammerDoesNotHave.Contains(x.Id));
            return Mapper.Map<IEnumerable<Skill>, IEnumerable<SkillDTO>>(skillsProgrammerDoesNotHave);
        }
        public void Insert(SkillDTO skillDto)
        {
            var skill = Database.Skills.GetAll().Where(x => x.Name == skillDto.Name).FirstOrDefault();
            if (skill != null)
                throw new ValidationException("This skill already exists", "Name");
            Database.Skills.Insert(Mapper.Map<SkillDTO, Skill>(skillDto));
            Database.Save();
        }

        public void Delete(int id)
        {
            var skill = Database.Skills.Get(id);
            if(skill == null)
                throw new ValidationException("This skill has not found", "Id");
            Database.Skills.Delete(skill.Id);
            Database.Save();
        }

        public void Update(int skillId, SkillDTO skillDTO)
        {
            if (skillId != skillDTO.Id)
                throw new ValidationException("Skill's id don't match", "Id");
            var skill = Database.Skills.Get(skillDTO.Id);
            if (skill == null)
                throw new ValidationException("This skill has not found", "Id");
            Database.Skills.Update(Mapper.Map<SkillDTO, Skill>(skillDTO));
            Database.Save();
        }
    }
}
