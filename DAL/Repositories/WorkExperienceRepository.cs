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
    public class WorkExperienceRepository : IRepository<WorkExperience, int>
    {
        private KnowledgeAccountingContext db;
        public WorkExperienceRepository(KnowledgeAccountingContext context)
        {
            this.db = context;

        }
        public void Delete(int id)
        {
            WorkExperience skill = db.WorkExperiences.Find(id);
            if (skill != null)
                db.WorkExperiences.Remove(skill);
        }

        public WorkExperience Get(int id)
        {
            return db.WorkExperiences.Find(id);
        }

        public IEnumerable<WorkExperience> GetAll()
        {
            return db.WorkExperiences;
        }

        public void Insert(WorkExperience workExperience)
        {
            db.WorkExperiences.Add(workExperience);
        }

        public void Update(WorkExperience workExperience)
        {
            var localEntity = db.WorkExperiences.Local.FirstOrDefault(x => x.Id == workExperience.Id);
            if (localEntity != null)
            {
                db.Entry(localEntity).State = EntityState.Detached;
            }
            db.Entry(workExperience).State = EntityState.Modified;
        }
    }
}
