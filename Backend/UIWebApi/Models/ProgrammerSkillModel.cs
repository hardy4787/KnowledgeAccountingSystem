using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UIWebApi.Models
{
    public class ProgrammerSkillModel
    {
        public int? SkillId { get; set; }
        [Required]
        public int KnowledgeLevel { get; set; }
        [Required]
        public string ProgrammerId { get; set; }
    }
}