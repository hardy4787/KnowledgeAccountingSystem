using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UIWebApi.Models
{
    public class WorkExperienceModel
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public string Description { get; set; }
        public string ProgrammerId { get; set; }
    }
}