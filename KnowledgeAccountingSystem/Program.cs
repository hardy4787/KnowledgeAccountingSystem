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

            Console.WriteLine("Товары заданной категорииkekаыа:");
            foreach (var item in DbProduct.GetAll())
                System.Console.WriteLine(item.Id + " " + item.FullName + " " + item.Age);
            Console.WriteLine();

            DbProduct.Insert(new BLL.DTO.ProgrammerDTO { FullName = "Петух" });
        }
    }
}
