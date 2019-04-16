using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    /// <summary>
    /// Service for working with education.
    /// </summary>
    public interface IEducationService
    {
        /// <summary>
        /// Get education by profile id.
        /// </summary>
        /// <param name="id">Id of profile associated with the education</param>
        IEnumerable<EducationDTO> GetEducationByProfileId(string id);
        /// <summary>
        /// Insert education to profile.
        /// </summary>
        /// <param name="education">Education which will be added</param>
        void Insert(EducationDTO education);
        /// <summary>
        /// Updated education of profile.
        /// </summary>
        /// <param name="educationId">Id of education which match with id updated Education
        /// <param name="education">Education which will be updated</param>
        void Update(int educationId, EducationDTO education);
        /// <summary>
        /// Delete education from profile.
        /// </summary>
        /// <param name="educationId">Id of education which will be deleted</param>    
        void Delete(int educationId);
    }
}
