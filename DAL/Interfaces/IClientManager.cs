using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IClientManager
    {
        void Create(ProgrammerProfile programmer);
        ProgrammerProfile Get(string id);
        IEnumerable<ProgrammerProfile> GetAll();
        void Delete(string id);
    }
}
