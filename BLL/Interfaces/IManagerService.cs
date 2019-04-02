using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IManagerService
    {
        IEnumerable<ProgrammerProfileDTO> GetAllProgramers();
        IEnumerable<ProgrammerProfileDTO> GetProgrammersBySkill(int? idSkill, int knowledgeLevel);
    }
}
