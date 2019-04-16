using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UIWebApi.Models
{
    public class ProjectModel
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ReferenceToTheProject { get; set; }
        [Required]
        [MaxLength(1024)]
        public string DescriptionOfTasks { get; set; }
        [Required]
        public string ProgrammerId { get; set; }
    }
}