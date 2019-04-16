using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Ninject
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IProgrammerProfileService>().To<ProgrammerProfileService>();
            Bind<ISkillService>().To<SkillService>();
            Bind<IWorkExperienceService>().To<WorkExperienceService>();
            Bind<IEducationService>().To<EducationService>();
            Bind<IProjectService>().To<ProjectService>();
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument("DefaultConnection");
        }
    }
}
