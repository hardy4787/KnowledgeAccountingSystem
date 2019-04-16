using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class ProjectConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(64);
            Property(x => x.DescriptionOfTasks).IsRequired().HasMaxLength(1024);
                HasRequired(x => x.ProgrammerProfile)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.ProgrammerId);
        }
    }
}
