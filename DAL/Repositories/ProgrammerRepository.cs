using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProgrammerRepository : IRepository<Programmer>
    {
        private KnowledgeAccountingContext db;
        public ProgrammerRepository(KnowledgeAccountingContext context)
        {
            this.db = context;

        }
        public void Delete(int id)
        {
            Programmer programmer = db.Programmers.Find(id);
            if (programmer != null)
                db.Programmers.Remove(programmer);
        }

        public Programmer Get(int id)
        {
            return db.Programmers.Find(id);
        }

        public IEnumerable<Programmer> GetAll()
        {
            return db.Programmers;
        }

        public void Insert(Programmer programmer)
        {
            db.Programmers.Add(programmer);
        }

        public void Update(Programmer programmer)
        {
            db.Entry(programmer).State = EntityState.Modified;
        }
    }
}
