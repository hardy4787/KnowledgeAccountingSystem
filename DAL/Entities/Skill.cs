using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ProgrammerSkill> ProgrammerSkills { get; set; }
        public Skill()
        {
            ProgrammerSkills = new List<ProgrammerSkill>();
        }
    }
}
