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
        ProgrammerProfileDTO Get(string id);
        void Update(string userId, ProgrammerProfileDTO item);
        void UpdateImageProfileUrl(string url, string fileType, int fileSize, string id);
        IEnumerable<ProgrammerProfileDTO> GetProgrammersBySkill(int? idSkill, int knowledgeLevel);
        byte[] GenerateReport(List<ProgrammerProfileDTO> profiles);

    }
}
