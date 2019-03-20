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
        IRepository<Programmer> Programmers { get; }
        IRepository<Skill> Skills { get; }
        IRepository<Education> Educations { get; }
        IRepository<PerformedTask> PerformedTasks { get; }
        IRepository<Project> Projects { get; }
        IRepository<ProgrammerSkill> ProgrammerSkills { get; }
        void Save();
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        Task SaveAsync();
    }
}
