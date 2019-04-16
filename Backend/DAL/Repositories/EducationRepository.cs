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
    public class EducationRepository : IRepository<Education, int>
    {
        private KnowledgeAccountingContext db;
        public EducationRepository(KnowledgeAccountingContext context)
        {
            this.db = context;
        }
        public void Delete(int id)
        {
            Education education = db.Educations.Find(id);
            if (education != null)
                db.Educations.Remove(education);
        }

        public Education Get(int id)
        {
            return db.Educations.Find(id);
        }

        public IEnumerable<Education> GetAll()
        {
            return db.Educations;
        }

        public void Insert(Education education)
        {
            db.Educations.Add(education);
        }

        public void Update(Education education)
        {
            var localEntity = db.Educations.Local.FirstOrDefault(x => x.Id == education.Id);
            if (localEntity != null)
            {
                db.Entry(localEntity).State = EntityState.Detached;
            }
            db.Entry(education).State = EntityState.Modified;
        }
    }
}
