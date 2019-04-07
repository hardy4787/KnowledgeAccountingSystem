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

        [Route("{id}/projects")]
        public IHttpActionResult GetProjects(string id)
        {
            IEnumerable<ProjectModel> projects;
            try
            {
                projects = Mapper.Map<IEnumerable<ProjectDTO>, IEnumerable<ProjectModel>>(_projectService.GetProjectsByProfileId(id));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(projects);
        }

        [Route("{id}/projects")]
        [HttpPost]
        public IHttpActionResult AddProject([FromBody]ProjectModel project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _projectService.Insert(Mapper.Map<ProjectModel, ProjectDTO>(project));
            return Ok(new { message = "Project added successfully" });
        }

        [Route("{id}/projects/{projectId}")]
        [HttpPut]
        public IHttpActionResult UpdateProject(int projectId, [FromBody]ProjectModel project)
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
            return Ok(new { message = "Project changed successfully" });
        }

        [Route("{id}/projects/{projectId}")]
        public IHttpActionResult Delete(int projectId)
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
            return Ok(new { message = "Project deleted successfully" });
        }
    }
}
