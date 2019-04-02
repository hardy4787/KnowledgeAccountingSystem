using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UIWebApi.Models;

namespace UIWebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/profile")] 
    public class ProjectController : ApiController
    {
        private IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Route("{id}/projects")]
        public IEnumerable<ProjectModel> Get(string id)
        {
            return Mapper.Map<IEnumerable<ProjectDTO>, IEnumerable<ProjectModel>>(_projectService.GetProjectByProfileId(id));
        }

        [Route("{id}/projects")]
        public void Post([FromBody]ProjectModel project)
        {
            _projectService.Insert(Mapper.Map<ProjectModel, ProjectDTO>(project));
        }

        [Route("{id}/projects/{projectId}")]
        public void Put(int projectId, [FromBody]ProjectModel project)
        {
            if (projectId == project.Id)
            {
                _projectService.Update(Mapper.Map<ProjectModel, ProjectDTO>(project));
            }
        }

        [Route("{id}/projects/{projectId}")]
        public void Delete(int projectId)
        {
            _projectService.Delete(projectId);
        }
    }
}
