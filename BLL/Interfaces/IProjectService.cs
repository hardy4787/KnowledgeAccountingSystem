using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    /// <summary>
    /// Service for working with project.
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Get projects by profile id.
        /// </summary>
        /// <param name="id">Id of profile associated with the project</param>
        IEnumerable<ProjectDTO> GetProjectsByProfileId(string id);
        /// <summary>
        /// Insert projects to profile.
        /// </summary>
        /// <param name="project">Project which will be added</param>
        void Insert(ProjectDTO project);
        /// <summary>
        /// Updated project of profile.
        /// </summary>
        /// <param name="id">Id of project which match with id updated Project
        /// <param name="projectId">Project which will be updated</param>
        void Update(int projectId, ProjectDTO project);
        /// <summary>
        /// Delete project from profile.
        /// </summary>
        /// <param name="projectId">Id of project which will be deleted</param>  
        void Delete(int projectId);
    }
}
