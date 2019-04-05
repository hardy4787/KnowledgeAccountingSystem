using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class EducationConfiguration : EntityTypeConfiguration<Education>
    {
        public EducationConfiguration()
        {
            Property(x => x.NameInstitution).IsRequired().HasMaxLength(64);
            Property(x => x.EntryDate).HasColumnType("date");
            Property(x => x.CloseDate).HasColumnType("date").IsOptional();
            HasRequired(x => x.ProgrammerProfile)
            .WithMany(x => x.Educations)
            .HasForeignKey(x => x.ProgrammerId);
        }
    }
}
