using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    /// <summary>
    /// Service for working with work experience.
    /// </summary>
    public interface IWorkExperienceService
    {
        /// <summary>
        /// Get work experience by profile id.
        /// </summary>
        /// <param name="id">Id of profile associated with the work experience</param>
        IEnumerable<WorkExperienceDTO> GetWorkExperienceByProfileId(string id);
        /// <summary>
        /// Insert work experience to profile.
        /// </summary>
        /// <param name="workExperience">Work experience which will be added</param>
        void Insert(WorkExperienceDTO workExperience);
        /// <summary>
        /// Update work experience of profile.
        /// </summary>
        /// <param name="workExperienceId">Id of work experience which match with id updated work experience
        /// <param name="workExperience">Work experience which will be added</param>
        void Update(int workExperienceId, WorkExperienceDTO workExperience);
        /// <summary>
        /// Delete work experience from profile.
        /// </summary>
        /// <param name="workExperienceId">Id of work experience which will be deleted</param>  
        void Delete(int workExperienceId);
    }
}
