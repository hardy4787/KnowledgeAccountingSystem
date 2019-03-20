using BLL.Services;
using DAL.Repositories;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using Util.Ninject;

namespace KnowledgeAccountingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<ApplicationProfile>());

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            ProgrammerService DbProduct = new ProgrammerService(new EFUnitOfWork(connectionString));

            foreach (var item in DbProduct.GetBySkill(1))
                System.Console.WriteLine(item.Id + " " + item.FullName + " " + item.Age);
            Console.WriteLine();
        }
    }
}
