using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISkillService
    {
        IEnumerable<ProgrammerSkillDTO> GetSkillsOfProgrammer(string id);
        IEnumerable<SkillDTO> GetSkillsThatTheProgrammerDoesNotHave(string id);
        void InsertSkillToProgrammer(ProgrammerSkillDTO skill);
        void UpdateSkillOfProgrammer(int skillId, ProgrammerSkillDTO skill);
        void DeleteSkillOfProgrammer(string idProgrammer, int idSkill);
        IEnumerable<SkillDTO> GetSkills();
        void Insert(SkillDTO skill);
        void Delete(int id);
        void Update(int skillId, SkillDTO skill);
    }
}
