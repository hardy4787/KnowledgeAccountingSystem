using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<TClass, TId> where TClass : class
    {
        IEnumerable<TClass> GetAll();
        TClass Get(TId id);
        void Insert(TClass item);
        void Update(TClass item);
        void Delete(TId id);
    }
}
