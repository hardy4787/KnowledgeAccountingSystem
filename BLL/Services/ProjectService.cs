using AutoMapper;
using BLL.DTO;
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
        IUnitOfWork Database { get; set; }
        public ProjectService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public IEnumerable<ProjectDTO> GetProjectByProfileId(string id)
        {
            var project = Database.Projects.GetAll().Where(x => x.ProgrammerId == id);
            return Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDTO>>(project);
        }


        public ProjectDTO Get(int id)
        {
            return Mapper.Map<Project, ProjectDTO>(Database.Projects.Get(id));
        }

        public void Insert(ProjectDTO project)
        {
            Database.Projects.Insert(Mapper.Map<ProjectDTO, Project>(project));
            Database.Save();
        }

        public void Update(ProjectDTO project)
        {
            Database.Projects.Update(Mapper.Map<ProjectDTO, Project>(project));
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Projects.Delete(id);
            Database.Save();
        }
    }
}
