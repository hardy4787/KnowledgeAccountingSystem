using DAL.EF;
using DAL.Entities;
using DAL.Identity;
using DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
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

        private ProgrammerProfileRepository programmerProfileRepository;
        private SkillRepository skillRepository;
        private ProjectRepository projectRepository;
        private EducationRepository educationRepository;
        private ProgrammerSkillRepository programmerSkillRepository;
        //private PerformedTaskRepository performedTaskRepository;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;

        public EFUnitOfWork(string connectionString)
        {
            db = new KnowledgeAccountingContext(connectionString);
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                if (userManager == null)
                    userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                return userManager;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (roleManager == null)
                    roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
                return roleManager;
            }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
        public IRepository<ProgrammerProfile, string> ProgrammerProfiles
        {
            get
            {
                if (programmerProfileRepository == null)
                    programmerProfileRepository = new ProgrammerProfileRepository(db);
                return programmerProfileRepository;
            }
        }
        public IProgrammerSkillRepository ProgrammerSkills
        {
            get
            {
                if (programmerSkillRepository == null)
                    programmerSkillRepository = new ProgrammerSkillRepository(db);
                return programmerSkillRepository;
            }
        }

        public IRepository<Skill, int> Skills
        {
            get
            {
                if (skillRepository == null)
                    skillRepository = new SkillRepository(db);
                return skillRepository;
            }
        }
        //public IRepository<PerformedTask> PerformedTasks
        //{
        //    get
        //    {
        //        if (performedTaskRepository == null)
        //            performedTaskRepository = new PerformedTaskRepository(db);
        //        return performedTaskRepository;
        //    }
        //}
        public IRepository<Project, int> Projects
        {
            get
            {
                if (projectRepository == null)
                    projectRepository = new ProjectRepository(db);
                return projectRepository;
            }
        }

        public IRepository<Education, int> Educations
        {
            get
            {
                if (educationRepository == null)
                    educationRepository = new EducationRepository(db);
                return educationRepository;
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

        ~EFUnitOfWork()
        {
            Dispose(false);
        }
    }
}
