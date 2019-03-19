using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private KnowledgeAccountingContext db;

        private ProgrammerRepository programmerRepository;
        private SkillRepository skillRepository;
        private ProjectRepository projectRepository;
        private PerformedTaskRepository performedTaskRepository;
        public EFUnitOfWork(string connectionString)
        {
            db = new KnowledgeAccountingContext(connectionString);
        }
        public IRepository<Programmer> Programmers
        {
            get
            {
                if (programmerRepository == null)
                    programmerRepository = new ProgrammerRepository(db);
                return programmerRepository;
            }
        }

        public IRepository<Skill> Skills
        {
            get
            {
                if (skillRepository == null)
                    skillRepository = new SkillRepository(db);
                return skillRepository;
            }
        }
        public IRepository<PerformedTask> PerformedTasks
        {
            get
            {
                if (performedTaskRepository == null)
                    performedTaskRepository = new PerformedTaskRepository(db);
                return performedTaskRepository;
            }
        }
        public IRepository<Project> Projects
        {
            get
            {
                if (projectRepository == null)
                    projectRepository = new ProjectRepository(db);
                return projectRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
