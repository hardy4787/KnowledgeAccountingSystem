using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ProgrammerProfile
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string GitHub { get; set; }
        public string Address { get; set; }
        public string ImageProfileUrl { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<ProgrammerSkill> ProgrammerSkills { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<WorkExperience> WorkExperiences { get; set; }
        public ProgrammerProfile()
        {
            ProgrammerSkills = new List<ProgrammerSkill>();
            Projects = new List<Project>();
            Educations = new List<Education>();
            WorkExperiences = new List<WorkExperience>();
        }
    }
}
