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

        [Key, Column(Order = 0)]
        public string ProgrammerId { get; set; }
        [Key, Column(Order = 1)]
        public int SkillId { get; set; }

        public virtual ProgrammerProfile Programmer { get; set; }
        public virtual Skill Skill { get; set; }

        public int KnowledgeLevel { get; set; }
    }
}
