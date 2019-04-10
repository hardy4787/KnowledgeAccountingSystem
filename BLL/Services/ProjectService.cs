using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProjectService : IProjectService
    {
        private IUnitOfWork Database { get; set; }
        public ProjectService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public IEnumerable<ProjectDTO> GetProjectsByProfileId(string id)
        {
            var programmer = Database.ProgrammerProfiles.Get(id);
            if (programmer == null)
                throw new ValidationException("Programmer has not found", "Id");
            var project = Database.Projects.GetAll().Where(x => x.ProgrammerId == id);
            return Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDTO>>(project);
        }

        public void Insert(ProjectDTO projectDTO)
        {
            var project = Database.Projects.Get(projectDTO.Id);
            if (project != null)
                throw new ValidationException("Project with this id already exists", "Id");
            Database.Projects.Insert(Mapper.Map<ProjectDTO, Project>(projectDTO));
            Database.Save();
        }

        public void Update(int projectId, ProjectDTO projectDTO)
        {
            if (projectId != projectDTO.Id)
                throw new ValidationException("Project's id don't match", "Id");
            var project = Database.Skills.Get(projectDTO.Id);
            if (project == null)
                Database.Projects.Update(Mapper.Map<ProjectDTO, Project>(projectDTO));
            Database.Save();
        }

        public void Delete(int id)
        {
            var project = Database.Projects.Get(id);
            if (project == null)
                throw new ValidationException("This project has not found", "Id");
            Database.Projects.Delete(id);
            Database.Save();
        }
    }
}
