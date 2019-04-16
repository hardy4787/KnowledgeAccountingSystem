using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
            ProgrammerProfile programmer = db.ProgrammerProfiles.Find(id);
            if (programmer != null)
                db.ProgrammerProfiles.Remove(programmer);
        }

        public ProgrammerProfile Get(string id)
        {
            return db.ProgrammerProfiles.Find(id);
        }

        public IEnumerable<ProgrammerProfile> GetAll()
        {
            return db.ProgrammerProfiles;
        }

        public void Insert(ProgrammerProfile programmer)
        {
            db.ProgrammerProfiles.Add(programmer);
        }

        public void Update(ProgrammerProfile programmer)
        {
            var localEntity = db.ProgrammerProfiles.Local.FirstOrDefault(x => x.Id == programmer.Id);
            if (localEntity != null)
            {
                db.Entry(localEntity).State = EntityState.Detached;
            }
            db.Entry(programmer).State = EntityState.Modified;
        }
    }
}
