using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IWorkExperienceService
    {
        IEnumerable<WorkExperienceDTO> GetWorkExperienceByProfileId(string id);
        void Insert(WorkExperienceDTO workExperience);
        void Update(int workExperienceId, WorkExperienceDTO workExperience);
        void Delete(int id);
    }
}
