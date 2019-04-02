//using DAL.EF;
//using DAL.Entities;
//using DAL.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DAL.Repositories
//{
//    public class PerformedTaskRepository : IRepository<PerformedTask>
//    {
//        private KnowledgeAccountingContext db;
//        public PerformedTaskRepository(KnowledgeAccountingContext context)
//        {
//            this.db = context;

//        }
//        public void Delete(int id)
//        {
//            PerformedTask performedTask = db.PerformedTasks.Find(id);
//            if (performedTask != null)
//                db.PerformedTasks.Remove(performedTask);
//        }

//        public PerformedTask Get(int id)
//        {
//            return db.PerformedTasks.Find(id);
//        }

//        public IEnumerable<PerformedTask> GetAll()
//        {
//            return db.PerformedTasks;
//        }

//        public void Insert(PerformedTask performedTask)
//        {
//            db.PerformedTasks.Add(performedTask);
//        }

//        public void Update(PerformedTask performedTask)
//        {
//            db.Entry(performedTask).State = EntityState.Modified;
//        }
//    }
//}
