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
        WorkExperienceDTO Get(int id);
        void Insert(WorkExperienceDTO workExperience);
        void Update(WorkExperienceDTO workExperience);
        void Delete(int id);
    }
}
