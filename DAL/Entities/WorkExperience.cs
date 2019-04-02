using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class WorkExperience
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public string NameInstitution { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime CloseDate { get; set; }
        public string ProgrammerId { get; set; }
        public virtual ProgrammerProfile Programmer { get; set; }
    }
}
