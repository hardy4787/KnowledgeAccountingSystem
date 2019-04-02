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
    public class ProgrammerProfileRepository : IRepository<ProgrammerProfile, string>
    {
        private KnowledgeAccountingContext db;
        public ProgrammerProfileRepository(KnowledgeAccountingContext context)
        {
            this.db = context;

        }
        public void Delete(string id)
        {
            ProgrammerProfile programmer = db.Programmers.Find(id);
            if (programmer != null)
                db.Programmers.Remove(programmer);
        }

        public ProgrammerProfile Get(string id)
        {
            return db.Programmers.Find(id);
        }

        public IEnumerable<ProgrammerProfile> GetAll()
        {
            return db.Programmers;
        }

        public void Insert(ProgrammerProfile programmer)
        {
            db.Programmers.Add(programmer);
        }

        public void Update(ProgrammerProfile programmer)
        {
            db.Entry(programmer).State = EntityState.Modified;
        }
    }
}
