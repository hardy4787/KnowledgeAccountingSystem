using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    class ProgrammerSkillConfiguration : EntityTypeConfiguration<ProgrammerSkill>
    {
        public ProgrammerSkillConfiguration() {
            HasKey(p => new { p.ProgrammerId, p.SkillId });
            Property(x => x.KnowledgeLevel).IsRequired();

            HasRequired(t => t.Skill)
            .WithMany(t => t.ProgrammerSkills)
            .HasForeignKey(t => t.SkillId);


            HasRequired(t => t.ProgrammerProfile)
            .WithMany(t => t.ProgrammerSkills)
            .HasForeignKey(t => t.ProgrammerId);
        }
    }
}
