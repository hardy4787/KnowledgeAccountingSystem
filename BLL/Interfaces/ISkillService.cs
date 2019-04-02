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
        void UpdateSkillOfProgrammer(ProgrammerSkillDTO skill);
        void DeleteSkillOfProgrammer(string idProgrammer, int idSkill);
        IEnumerable<SkillDTO> GetSkills();
    }
}
