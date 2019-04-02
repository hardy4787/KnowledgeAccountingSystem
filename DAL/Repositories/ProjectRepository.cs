using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProjectRepository : IRepository<Project, int>
    {
        private KnowledgeAccountingContext db;
        public ProjectRepository(KnowledgeAccountingContext context)
        {
            this.db = context;

        }
        public void Delete(int id)
        {
            Project project = db.Projects.Find(id);
            if (project != null)
                db.Projects.Remove(project);
        }

        public Project Get(int id)
        {
            return db.Projects.Find(id);
        }

        public IEnumerable<Project> GetAll()
        {
            return db.Projects;
        }

        public void Insert(Project project)
        {
            db.Projects.Add(project);
        }

        public void Update(Project project)
        {
            db.Entry(project).State = EntityState.Modified;
        }
    }
}
