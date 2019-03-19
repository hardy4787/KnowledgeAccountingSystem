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
            Bind<IProgrammerService>().To<ProgrammerService>().InSingletonScope();
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument("DefaultConnection");
        }
    }
}
