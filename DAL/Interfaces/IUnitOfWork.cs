using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Programmer> Programmers { get; }
        IRepository<Skill> Skills { get; }
        IRepository<PerformedTask> PerformedTasks { get; }
        IRepository<Project> Projects { get; }
        void Save();
    }
}
