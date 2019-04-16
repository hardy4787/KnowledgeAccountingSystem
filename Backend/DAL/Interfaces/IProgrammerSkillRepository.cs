using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProgrammerSkillRepository
    {
        IEnumerable<ProgrammerSkill> GetAll();
        ProgrammerSkill Get(string idProgrammer, int idSkill);
        void Insert(ProgrammerSkill item);
        void Update(ProgrammerSkill item);
        void Delete(string idProgrammer, int idSkill);
    }
}
