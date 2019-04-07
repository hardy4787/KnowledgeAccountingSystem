using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UIWebApi.Models
{
    public class ProjectModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string ReferenceToTheProject { get; set; }
        public string DescriptionOfTasks { get; set; }
        public string ProgrammerId { get; set; }
    }
}