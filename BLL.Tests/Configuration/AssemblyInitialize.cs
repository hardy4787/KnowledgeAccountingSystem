using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace BLL.Tests.Configuration
{
    [TestClass] //you have to label the class with this or it is never scanned for methods
    public class AssemblyInitialize
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<ApplicationProfile>());
        }
    }
}
