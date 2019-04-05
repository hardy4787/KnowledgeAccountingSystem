using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ReferenceToTheProject { get; set; }
        public string DescriptionOfTasks { get; set; }
        public string ProgrammerId { get; set; }
        public virtual ProgrammerProfile ProgrammerProfile { get; set; }
        //public virtual ICollection<PerformedTask> PerformedTasks { get; set; }
        //public Project()
        //{
        //    PerformedTasks = new List<PerformedTask>();
        //}
    }
}
