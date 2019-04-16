using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ProgrammerSkill
    {

        public string ProgrammerId { get; set; }
        public int SkillId { get; set; }

        public virtual ProgrammerProfile ProgrammerProfile { get; set; }
        public virtual Skill Skill { get; set; }

        public int KnowledgeLevel { get; set; }
    }
}
