using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class WorkExperienceConfiguration : EntityTypeConfiguration<WorkExperience>
    {
        public WorkExperienceConfiguration()
        {
            HasRequired(x => x.ProgrammerProfile)
                .WithMany(x => x.WorkExperiences)
                .HasForeignKey(x => x.ProgrammerId);
            Property(x => x.Company).IsRequired().HasMaxLength(64);
            Property(x => x.Position).IsRequired().HasMaxLength(64);
            Property(x => x.Description).IsRequired().HasMaxLength(1024);
            Property(x => x.EntryDate).HasColumnType("date");
            Property(x => x.CloseDate).HasColumnType("date").IsOptional();
        }
    }
}
