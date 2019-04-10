using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UIWebApi.Filters;
using UIWebApi.Models;

namespace UIWebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/profile")] 
    public class ProjectController : ApiController
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Route("{userId}/projects")]
        public IHttpActionResult GetProjects(string userId)
        {
            IEnumerable<ProjectModel> projects;
            try
            {
                projects = Mapper.Map<IEnumerable<ProjectDTO>, IEnumerable<ProjectModel>>(_projectService.GetProjectsByProfileId(userId));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(projects);
        }

        [AccessActionFilter]
        [Route("{userId}/projects")]
        [HttpPost]
        public IHttpActionResult AddProject(string userId, [FromBody]ProjectModel project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _projectService.Insert(Mapper.Map<ProjectModel, ProjectDTO>(project));
            return Ok(new { Message = "Project added successfully" });
        }

        [AccessActionFilter]
        [Route("{userId}/projects/{projectId}")]
        [HttpPut]
        public IHttpActionResult UpdateProject(string userId, int projectId, [FromBody]ProjectModel project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _projectService.Update(projectId, Mapper.Map<ProjectModel, ProjectDTO>(project));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(new { Message = "Project changed successfully" });
        }

        [AccessActionFilter]
        [Route("{userId}/projects/{projectId}")]
        public IHttpActionResult Delete(string userId, int projectId)
        {
            try
            {
                _projectService.Delete(projectId);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(new { Message = "Project deleted successfully" });
        }
    }
}
