using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProgrammerProfileService
    {
        IEnumerable<ProgrammerProfileDTO> GetAll();
        ProgrammerProfileDTO Get(string id);
        void Update(ProgrammerProfileDTO item);
        void UpdateImageProfileUrl(string url, string id);
        void DeleteOldImageProfile(string id);
        IEnumerable<ProgrammerProfileDTO> GetProgrammersBySkill(int? idSkill, int knowledgeLevel);
        byte[] GenerateReport(IEnumerable<ProgrammerProfileDTO> profiles);

    }
}
