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
    public class SkillRepository : IRepository<Skill, int>
    {
        private KnowledgeAccountingContext db;
        public SkillRepository(KnowledgeAccountingContext context)
        {
            this.db = context;

        }
        public void Delete(int id)
        {
            Skill skill = db.Skills.Find(id);
            if (skill != null)
                db.Skills.Remove(skill);
        }

        public Skill Get(int id)
        {
            return db.Skills.Find(id);
        }

        public IEnumerable<Skill> GetAll()
        {
            return db.Skills;
        }

        public void Insert(Skill skill)
        {
            db.Skills.Add(skill);
        }

        public void Update(Skill skill)
        {
            db.Entry(skill).State = EntityState.Modified;
        }
    }
}
