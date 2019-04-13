using DAL.Configurations;
using DAL.Entities;
using DAL.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class KnowledgeAccountingContext : IdentityDbContext
    {
        public KnowledgeAccountingContext(string connectionString) : base(connectionString) { }
        static KnowledgeAccountingContext()
        {
            Database.SetInitializer<KnowledgeAccountingContext>(new KnowledgeAccountingDbInitializer());
        }
        public DbSet<ProgrammerProfile> ProgrammerProfiles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<ProgrammerSkill> ProgrammerSkills { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProfileConfiguration());
            modelBuilder.Configurations.Add(new SkillConfiguration());
            modelBuilder.Configurations.Add(new EducationConfiguration());
            modelBuilder.Configurations.Add(new ProjectConfiguration());
            modelBuilder.Configurations.Add(new WorkExperienceConfiguration());
            modelBuilder.Configurations.Add(new ProgrammerSkillConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }

    public class KnowledgeAccountingDbInitializer : DropCreateDatabaseIfModelChanges<KnowledgeAccountingContext>
    {
        protected override void Seed(KnowledgeAccountingContext db)
        {
            #region Add roles
            List<string> roles = new List<string> { "user", "admin" };
            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(db);
            var roleManager = new RoleManager<ApplicationRole>(roleStore);
            foreach (string roleName in roles)
            {
                var role = new ApplicationRole { Name = roleName };
                roleManager.Create(role);
            }
            #endregion

            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            ApplicationUser admin = new ApplicationUser { UserName = "hardy4787", Email = "hardy@gmail.com", Id = "123" };
            userManager.Create(admin, "123456");
            userManager.AddToRole(admin.Id, "admin");

            ApplicationUser user = new ApplicationUser { UserName = "bodya64", Email = "hristich@gmail.com", Id = "1" };
            userManager.Create(user, "123456");
            userManager.AddToRole(user.Id, "user");

            user = new ApplicationUser { UserName = "basik", Email = "basenko@gmail.com", Id = "2" };
            userManager.Create(user, "123456");
            userManager.AddToRole(user.Id, "user");

            user = new ApplicationUser { UserName = "ukolov123", Email = "ukolov@gmail.com", Id = "3" };
            userManager.Create(user, "123456");
            userManager.AddToRole(user.Id, "user");

            user = new ApplicationUser { UserName = "yarik", Email = "yarkulov@gmail.com", Id = "4" };
            userManager.Create(user, "123456");
            userManager.AddToRole(user.Id, "user");


            #region Create education
            List<Education> educations = new List<Education>
            {
                new Education{NameInstitution = "KPI", Level = "High", ProgrammerId = "1", EntryDate = new DateTime(2013,9,1), CloseDate=new DateTime(2017,6,20) },
                new Education{NameInstitution = "NAY", Level = "High", ProgrammerId = "2", EntryDate = new DateTime(2016,9,1), CloseDate=new DateTime(2020,6,20) },
                new Education{NameInstitution = "University Ukraine", Level = "High", ProgrammerId = "3", EntryDate = new DateTime(2008,9,1), CloseDate=new DateTime(2014,6,20) },
                new Education{NameInstitution = "Shevchenko", Level = "High", ProgrammerId = "4", EntryDate = new DateTime(2009,9,1), CloseDate=new DateTime(2015,6,20) },
                new Education{NameInstitution = "KNTY", Level = "High", ProgrammerId = "4", EntryDate = new DateTime(2015,9,1), CloseDate=new DateTime(2019,6,20) }
            };

            foreach (Education education in educations)
                db.Educations.Add(education);

            #endregion

            #region Create work experience
            List<WorkExperience> workExperiences = new List<WorkExperience>
            {
                new WorkExperience
                {
                    Company = "Google", Position ="Front-end developer", EntryDate=new DateTime(2015,9,1), CloseDate = new DateTime(2017,5,6), Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas imperdiet, ligula eu varius mollis, ligula metus aliquet ante, a rutrum libero sapien malesuada arcu.", ProgrammerId = "1"
                },
                new WorkExperience
                {
                    Company = "Pinterest", Position ="Junior Java developer", EntryDate=new DateTime(2017,9,1), CloseDate = new DateTime(2018,5,6), Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas imperdiet, ligula eu varius mollis, ligula metus aliquet ante, a rutrum libero sapien malesuada arcu.", ProgrammerId = "1"
                },
                new WorkExperience
                {
                    Company = "Epam", Position ="Junior .Net developer", EntryDate=new DateTime(2011,9,1), CloseDate = new DateTime(2015,5,6), Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas imperdiet, ligula eu varius mollis, ligula metus aliquet ante, a rutrum libero sapien malesuada arcu.", ProgrammerId = "2"
                },
                new WorkExperience
                {
                    Company = "TaoBao", Position =".Net Architector", EntryDate=new DateTime(2012,9,1), CloseDate = new DateTime(2013,5,6), Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas imperdiet, ligula eu varius mollis, ligula metus aliquet ante, a rutrum libero sapien malesuada arcu.", ProgrammerId = "4"
                },
                new WorkExperience
                {
                    Company = "NetCracker", Position ="Front-end developer", EntryDate=new DateTime(2013,9,1), CloseDate = new DateTime(2014,5,6), Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas imperdiet, ligula eu varius mollis, ligula metus aliquet ante, a rutrum libero sapien malesuada arcu.", ProgrammerId = "4"
                },
                new WorkExperience
                {
                    Company = "SoftServe", Position ="Junior Python developer", EntryDate=new DateTime(2015,9,1), CloseDate = new DateTime(2017,5,6), Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas imperdiet, ligula eu varius mollis, ligula metus aliquet ante, a rutrum libero sapien malesuada arcu.", ProgrammerId = "4"
                }

            };
            foreach (WorkExperience workExperience in workExperiences)
                db.WorkExperiences.Add(workExperience);

            #endregion

            #region Create projects
            List<Project> projects = new List<Project>
            {
                new Project
                {
                    Name = "TelegramX", ReferenceToTheProject = "https://telegram.org/", DescriptionOfTasks="I designed the personal file panel for the mobile version of the application.", ProgrammerId = "1"
                },
                new Project
                {
                    Name = "Smart House", ReferenceToTheProject = "http://www.smarthouse.ua/ru/", DescriptionOfTasks="Developed an automatic alarm system.", ProgrammerId = "1"
                },
                new Project
                {
                    Name = "Help Desk Operations", ReferenceToTheProject = "https://telegram.org/", DescriptionOfTasks="Developed project architecture.", ProgrammerId = "2"
                },
                new Project
                {
                    Name = "Doctor Antivirus", ReferenceToTheProject = "https://www.drweb.ru/", DescriptionOfTasks="Set up a payment system.", ProgrammerId = "2"
                },
                new Project
                {
                    Name = "Move Plan", ReferenceToTheProject = "https://moveplangroup.com/", DescriptionOfTasks="I made an application for counting calories and steps for the day..", ProgrammerId = "3"
                },
                new Project
                {
                    Name = "IT Compliance", ReferenceToTheProject = "https://telegram.org/", DescriptionOfTasks="Made a system of counting visitors on the site.", ProgrammerId = "3"
                },
                new Project
                {
                    Name = "Management", ReferenceToTheProject = "https://telegram.org/", DescriptionOfTasks="I made an application for counting calories and steps for the day.", ProgrammerId = "4"
                },
                new Project
                {
                    Name = "Vendor Selection", ReferenceToTheProject = "https://telegram.org/", DescriptionOfTasks="I designed the personal file panel for the mobile version of the application.", ProgrammerId = "4"
                }
            };

            foreach (Project project in projects)
                db.Projects.Add(project);

            #endregion

            #region Create programmer skill
            List<ProgrammerSkill> programmerSkills = new List<ProgrammerSkill>
            {
                new ProgrammerSkill{ProgrammerId = "1", SkillId = 1, KnowledgeLevel = 20},
                new ProgrammerSkill{ProgrammerId = "1", SkillId = 2, KnowledgeLevel = 50},
                new ProgrammerSkill{ProgrammerId = "2", SkillId = 1, KnowledgeLevel = 70},
                new ProgrammerSkill{ProgrammerId = "2", SkillId = 2, KnowledgeLevel = 100},
                new ProgrammerSkill{ProgrammerId = "2", SkillId = 3, KnowledgeLevel = 45},
                new ProgrammerSkill{ProgrammerId = "3", SkillId = 4, KnowledgeLevel = 45},
                new ProgrammerSkill{ProgrammerId = "3", SkillId = 1, KnowledgeLevel = 60},
                new ProgrammerSkill{ProgrammerId = "3", SkillId = 7, KnowledgeLevel = 100},
                new ProgrammerSkill{ProgrammerId = "4", SkillId = 6, KnowledgeLevel = 100},
                new ProgrammerSkill{ProgrammerId = "4", SkillId = 5, KnowledgeLevel = 20},
                new ProgrammerSkill{ProgrammerId = "4", SkillId = 8, KnowledgeLevel = 70},
                new ProgrammerSkill{ProgrammerId = "4", SkillId = 9, KnowledgeLevel = 80},
                new ProgrammerSkill{ProgrammerId = "4", SkillId = 10, KnowledgeLevel = 70}
            };

            foreach (ProgrammerSkill programmerSkill in programmerSkills)
                db.ProgrammerSkills.Add(programmerSkill);
            #endregion

            #region Create skills
            List<Skill> skills = new List<Skill>
            {
                new Skill{ Name = "C#", Description = "Language", ProgrammerSkills = new List<ProgrammerSkill>{ programmerSkills[0], programmerSkills[2], programmerSkills[6]} },
                new Skill{ Name = "Java", Description = "Language", ProgrammerSkills = new List<ProgrammerSkill>{ programmerSkills[1], programmerSkills[3] } },
                new Skill{ Name = "SQL", Description = "Writing of queries", ProgrammerSkills = new List<ProgrammerSkill>{ programmerSkills[4]}},
                new Skill{ Name = "HTML CSS", Description = "Creating front of cites", ProgrammerSkills = new List<ProgrammerSkill>{ programmerSkills[5]} },
                new Skill{ Name = "Python", Description = "Language", ProgrammerSkills = new List<ProgrammerSkill>{ programmerSkills[9]}},
                new Skill{ Name = "PHP", Description = "Language", ProgrammerSkills = new List<ProgrammerSkill>{ programmerSkills[8]}},
                new Skill{ Name = "GO", Description = "Language", ProgrammerSkills = new List<ProgrammerSkill>{ programmerSkills[7]}},
                new Skill{ Name = "Native Javascript", Description = "Language", ProgrammerSkills = new List<ProgrammerSkill>{ programmerSkills[10]}},
                new Skill{ Name = "Angular", Description = "TS framework", ProgrammerSkills = new List<ProgrammerSkill>{ programmerSkills[11]}},
                new Skill{ Name = "React", Description = "JS framework", ProgrammerSkills = new List<ProgrammerSkill>{ programmerSkills[12]}}
            };
            foreach (Skill skill in skills)
                db.Skills.Add(skill);
            #endregion
            #region Create users
            List<ProgrammerProfile> programmerProfiles = new List<ProgrammerProfile>
            {
                new ProgrammerProfile{ Id = "1", FullName = "Hristich Bogdan", Age = 21, Address = "Kiev", Email = "hristich@gmail.com", GitHub = "hardy4787", Phone = "0671605738", ImageProfileUrl = "/assets/image-profiles/fdfsdaf.png"},
                new ProgrammerProfile{ Id = "2", FullName = "Basenko Ivan", Age = 21, Address = "Kiev", Email = "basenko@gmail.com", GitHub = "chebyrek", Phone = "0960635124", ImageProfileUrl = "/assets/image-profiles/fsdfdsffdsf.png"},
                new ProgrammerProfile{ Id = "3", FullName = "Ukolov Nazar", Age = 25, Address = "Lviv", Email = "ukolov@gmail.com", GitHub = "ukolov777", Phone = "0672155745", ImageProfileUrl = "/assets/image-profiles/sdasdasd.png"},
                new ProgrammerProfile{ Id = "4", FullName = "Yarkulov Nazar", Age = 27, Address = "Kharkiv", Email = "yarkulov@gmail.com", GitHub = "yarkulov123", Phone = "0672155712", ImageProfileUrl = "/assets/image-profiles/unnamed.png"},
            };
            //List<ProgrammerProfile> programmerProfiles = new List<ProgrammerProfile>
            //{
            //    new ProgrammerProfile{ Id = "1", FullName = "Hristich Bogdan", Age = 21, Address = "Kiev", Email = "hristich@gmail.com", GitHub = "hardy4787", Phone = "0671605738", ImageProfileUrl = "/assets/image-profiles/fdfsdaf.png",
            //    Educations = new List<Education>{ educations[0] },
            //    Projects = new List<Project>{projects[0], projects[1] },
            //    WorkExperiences = new List<WorkExperience>{workExperiences[0], workExperiences[1] },
            //    ProgrammerSkills = new List<ProgrammerSkill>{ programmerSkills[0], programmerSkills[1] } },
            //    new ProgrammerProfile{ Id = "2", FullName = "Basenko Ivan", Age = 21, Address = "Kiev", Email = "basenko@gmail.com", GitHub = "chebyrek", Phone = "0960635124", ImageProfileUrl = "/assets/image-profiles/fsdfdsffdsf.png",
            //    Educations = new List<Education>{ educations[1] },
            //    Projects = new List<Project>{projects[2], projects[3] },
            //    WorkExperiences = new List<WorkExperience>{workExperiences[2] },
            //    ProgrammerSkills = new List<ProgrammerSkill>{ programmerSkills[2], programmerSkills[3], programmerSkills[4] } },
            //    new ProgrammerProfile{ Id = "3", FullName = "Ukolov Nazar", Age = 25, Address = "Lviv", Email = "ukolov@gmail.com", GitHub = "ukolov777", Phone = "0672155745", ImageProfileUrl = "/assets/image-profiles/sdasdasd.png",
            //    Educations = new List<Education>{ educations[2] },
            //    Projects = new List<Project>{projects[3], projects[4] },
            //    WorkExperiences = null,
            //    ProgrammerSkills = new List<ProgrammerSkill>{ programmerSkills[5], programmerSkills[6], programmerSkills[7] } },
            //    new ProgrammerProfile{ Id = "4", FullName = "Yarkulov Nazar", Age = 27, Address = "Kharkiv", Email = "yarkulov@gmail.com", GitHub = "yarkulov123", Phone = "0672155712", ImageProfileUrl = "/assets/image-profiles/unnamed.png",
            //    Educations = new List<Education>{ educations[3],educations[4] },
            //    Projects = new List<Project>{projects[5], projects[6] },
            //    WorkExperiences = new List<WorkExperience>{workExperiences[3], workExperiences[4], workExperiences[5] },
            //    ProgrammerSkills = new List<ProgrammerSkill>{ programmerSkills[8], programmerSkills[9] , programmerSkills[10], programmerSkills[11], programmerSkills[12] } },
            //};

            foreach (ProgrammerProfile programmerProfile in programmerProfiles)
                db.ProgrammerProfiles.Add(programmerProfile);
            #endregion

            db.SaveChanges();
        }
    }
}
//#region ApplicationUser
//modelBuilder.Entity<ProgrammerProfile>()
//                .HasRequired(c => c.ApplicationUser)
//                .WithOptional(c => c.ProgrammerProfile);
//#endregion

//#region ProgrammerProfile
//modelBuilder.Entity<ProgrammerProfile>().Property(p => p.FullName).IsRequired();
//modelBuilder.Entity<ProgrammerProfile>().Property(p => p.FullName).HasMaxLength(40);
//#endregion

//#region ProgrammerSkill
//modelBuilder.Entity<ProgrammerSkill>().HasKey(p => new { p.ProgrammerId, p.SkillId });

//            modelBuilder.Entity<ProgrammerSkill>()
//           .HasRequired(t => t.Skill)
//           .WithMany(t => t.ProgrammerSkills)
//           .HasForeignKey(t => t.SkillId);

//modelBuilder.Entity<ProgrammerSkill>()
//            .HasRequired(t => t.ProgrammerProfile)
//            .WithMany(t => t.ProgrammerSkills)
//            .HasForeignKey(t => t.ProgrammerId);
//#endregion

//#region Education

//modelBuilder.Entity<Education>()
//                .HasRequired(x => x.ProgrammerProfile)
//                .WithMany(x => x.Educations)
//                .HasForeignKey(x => x.ProgrammerId);
//#endregion

//#region Project

//modelBuilder.Entity<Project>()
//                .HasRequired(x => x.ProgrammerProfile)
//                .WithMany(x => x.Projects)
//                .HasForeignKey(x => x.ProgrammerId);
//#endregion

//#region WorkExperience
//modelBuilder.Entity<WorkExperience>()
//                .HasRequired(x => x.ProgrammerProfile)
//                .WithMany(x => x.WorkExperiences)
//                .HasForeignKey(x => x.ProgrammerId);
//            #endregion

//            base.OnModelCreating(modelBuilder);