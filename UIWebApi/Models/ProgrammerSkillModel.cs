using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UIWebApi.Models
{
    public class ProgrammerSkillModel
    {
        public int? SkillId { get; set; }
        public int KnowledgeLevel { get; set; }
        public string ProgrammerId { get; set; }
    }
}