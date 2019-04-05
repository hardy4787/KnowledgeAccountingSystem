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
            Bind<IProgrammerProfileService>().To<ProgrammerProfileService>().InSingletonScope();
            Bind<ISkillService>().To<SkillService>().InSingletonScope();
            Bind<IManagerService>().To<ManagerService>().InSingletonScope();
            Bind<IWorkExperienceService>().To<WorkExperienceService>().InSingletonScope();
            Bind<IEducationService>().To<EducationService>().InSingletonScope();
            Bind<IProjectService>().To<ProjectService>().InSingletonScope();
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument("DefaultConnection");
        }
    }
}
