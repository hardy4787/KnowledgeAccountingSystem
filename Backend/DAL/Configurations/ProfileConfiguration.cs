using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class ProfileConfiguration : EntityTypeConfiguration<ProgrammerProfile>
    {
        public ProfileConfiguration()
        {
            Property(p => p.FullName).IsRequired().HasMaxLength(64);
            Property(p => p.Email).IsRequired().HasMaxLength(64);
            Property(p => p.Address).HasMaxLength(64);
            Property(p => p.GitHub).HasMaxLength(64);
            HasRequired(c => c.ApplicationUser)
                .WithOptional(c => c.ProgrammerProfile);
        }
    }
}
