using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Identity;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<ProgrammerProfile, string> ProgrammerProfiles { get; }
        IRepository<Skill, int> Skills { get; }
        IRepository<Education, int> Educations { get; }
        //IRepository<PerformedTask> PerformedTasks { get; }
        IRepository<Project, int> Projects { get; }
        IProgrammerSkillRepository ProgrammerSkills { get; }
        void Save();
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        Task SaveAsync();
    }
}
