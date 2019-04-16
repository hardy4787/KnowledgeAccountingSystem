using DAL.EF;
using DAL.Entities;
using DAL.Identity;
using DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
        private WorkExperienceRepository workExperienceRepository;

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
        public IRepository<WorkExperience, int> WorkExperiences
        {
            get
            {
                if (workExperienceRepository == null)
                    workExperienceRepository = new WorkExperienceRepository(db);
                return workExperienceRepository;
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
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                );
            }
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
