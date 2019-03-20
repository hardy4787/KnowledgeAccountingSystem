using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime CloseDate { get; set; }
        public string ProgrammerId { get; set; }
        public virtual Programmer Programmer { get; set; }
        public virtual ICollection<PerformedTask> PerformedTasks { get; set; }
        public Project()
        {
            PerformedTasks = new List<PerformedTask>();
        }
    }
}
