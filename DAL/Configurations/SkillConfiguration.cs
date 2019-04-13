using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class SkillConfiguration : EntityTypeConfiguration<Skill>
    {
        public SkillConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(64);
            Property(p => p.Description).HasMaxLength(512);
        }
    }
}
