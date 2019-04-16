using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    /// <summary>
    /// Service for working with profile.
    /// </summary>
    public interface IProgrammerProfileService
    {
        /// <summary>
        /// Get profile by profile id.
        /// </summary>
        /// <param name="id">Id of profile with will be returned</param>
        ProgrammerProfileDTO Get(string id);
        /// <summary>
        /// Updated profile.
        /// </summary>
        /// <param name="userId">Id of profile with will be updated</param>
        /// <param name="profile">Profile with will be updated</param>
        void Update(string userId, ProgrammerProfileDTO profile);
        /// <summary>
        /// Updated image of profile.
        /// </summary>
        /// <param name="url">Url of image</param>
        /// <param name="fileType">Profile with will be updated</param>
        /// <param name="fileSize">Size of image</param>
        /// <param name="id">Id of profile</param>
        void UpdateImageProfileUrl(string url, string fileType, int fileSize, string id);
        /// <summary>
        /// Get profile by skills.
        /// </summary>
        /// <param name="idSkill">Id skill for filter of profiles</param>
        /// <param name="knowledgeLevel">Level of the skill</param>
        IEnumerable<ProgrammerProfileDTO> GetProgrammersBySkill(int? idSkill, int knowledgeLevel);
        /// <summary>
        /// Generate report by profiles.
        /// </summary>
        /// <param name="profiles">List profiles which will be in table of the Excel</param>
        byte[] GenerateReport(List<ProgrammerProfileDTO> profiles);

    }
}
