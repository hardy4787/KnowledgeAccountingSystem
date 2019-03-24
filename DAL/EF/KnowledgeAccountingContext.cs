using DAL.Entities;
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
        //static KnowledgeAccountingContext()
        //{
        //    Database.SetInitializer<KnowledgeAccountingContext>(new KnowledgeAccountingDbInitializer());
        //}
        public DbSet<ProgrammerProfile> Programmers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<ProgrammerSkill> ProgrammerSkills { get; set; }
        public DbSet<PerformedTask> PerformedTasks { get; set; }
    }
    //public class KnowledgeAccountingDbInitializer : DropCreateDatabaseIfModelChanges<KnowledgeAccountingContext>
    //{
    //    protected override void Seed(KnowledgeAccountingContext db)
    //    {
    //        List<Education> educations = new List<Education>
    //        {
    //            new Education{NameInstitution = "KPI", Level = "High", ProgrammerId = 1, EntryDate = new DateTime(2013,9,1), CloseDate=new DateTime(2017,6,20) },
    //            new Education{NameInstitution = "NAY", Level = "High", ProgrammerId = 2, EntryDate = new DateTime(2016,9,1), CloseDate=new DateTime(2020,6,20) },
    //            new Education{NameInstitution = "University Ukraine", Level = "High", ProgrammerId = 3, EntryDate = new DateTime(2008,9,1), CloseDate=new DateTime(2014,6,20) },
    //            new Education{NameInstitution = "Shevchenko", Level = "High", ProgrammerId = 4, EntryDate = new DateTime(2009,9,1), CloseDate=new DateTime(2015,6,20) },
    //            new Education{NameInstitution = "KNTY", Level = "High", ProgrammerId = 5, EntryDate = new DateTime(2015,9,1), CloseDate=new DateTime(2019,6,20) },
    //        };

    //        foreach (Education education in educations)
    //            db.Educations.Add(education);

    //        List<PerformedTask> performedTasks = new List<PerformedTask>
    //        {
    //            new PerformedTask{Name = "Front Panel", Description = "lalala", ProjectId = 1},
    //            new PerformedTask{Name = "Back Panel", Description = "oooooo", ProjectId = 1},
    //            new PerformedTask{Name = "Language Bar", Description = "ddsdasd", ProjectId = 2},
    //            new PerformedTask{Name = "Sound Player", Description = "sadsad", ProjectId = 3},
    //            new PerformedTask{Name = "Catched exceptions", Description = "asdsa", ProjectId = 4}
    //        };

    //        foreach (PerformedTask performedTask in performedTasks)
    //            db.PerformedTasks.Add(performedTask);

    //        List<Project> projects = new List<Project>
    //        {
    //            new Project
    //            {
    //                Name = "Telegram", EntryDate = new DateTime(2016, 10, 1), CloseDate = new DateTime(2017, 1, 2), ProgrammerId = 1, PerformedTasks = new List<PerformedTask>()
    //                {
    //                    performedTasks[0], performedTasks[1]
    //                }
    //            },
    //            new Project
    //            {
    //                Name = "Google", EntryDate = new DateTime(2017, 12, 1), CloseDate = new DateTime(2018, 1, 12), ProgrammerId = 2, PerformedTasks = new List<PerformedTask>()
    //                {
    //                    performedTasks[2]
    //                }
    //            },
    //            new Project
    //            {
    //                Name = "Yandex", EntryDate = new DateTime(2017, 11, 1), CloseDate = new DateTime(2017, 1, 23), ProgrammerId = 3, PerformedTasks = new List<PerformedTask>()
    //                {
    //                    performedTasks[3]
    //                }
    //            },
    //            new Project
    //            {
    //                Name = "Mongo", EntryDate = new DateTime(2016, 8, 1), CloseDate = new DateTime(2017, 11, 24), ProgrammerId = 4, PerformedTasks = new List<PerformedTask>()
    //                {
    //                    performedTasks[4]
    //                }
    //            }
    //        };

    //        foreach (Project project in projects)
    //            db.Projects.Add(project);

    //        List<ProgrammerSkill> programmerSkills = new List<ProgrammerSkill>
    //        {
    //            new ProgrammerSkill{ProgrammerId = 1, SkillId = 1, KnowledgeLevel = 20},
    //            new ProgrammerSkill{ProgrammerId = 1, SkillId = 2, KnowledgeLevel = 50},
    //            new ProgrammerSkill{ProgrammerId = 2, SkillId = 1, KnowledgeLevel = 70},
    //            new ProgrammerSkill{ProgrammerId = 2, SkillId = 2, KnowledgeLevel = 30},
    //            new ProgrammerSkill{ProgrammerId = 2, SkillId = 3, KnowledgeLevel = 45},
    //            new ProgrammerSkill{ProgrammerId = 3, SkillId = 1, KnowledgeLevel = 60},
    //            new ProgrammerSkill{ProgrammerId = 4, SkillId = 7, KnowledgeLevel = 100},
    //            new ProgrammerSkill{ProgrammerId = 4, SkillId = 5, KnowledgeLevel = 70}
    //        };

    //        foreach (ProgrammerSkill programmerSkill in programmerSkills)
    //            db.ProgrammerSkills.Add(programmerSkill);

    //        List<Skill> skills = new List<Skill>
    //        {
    //            new Skill{ Name = "C#", Description = "language", ProgrammerSkills = new List<ProgrammerSkill>(){ programmerSkills[0], programmerSkills[2], programmerSkills[5] } },
    //            new Skill{ Name = "Java", Description = "language", ProgrammerSkills = new List<ProgrammerSkill>(){ programmerSkills[1], programmerSkills[3] }  },
    //            new Skill{ Name = "SQL", Description = "Writing of queries", ProgrammerSkills = new List<ProgrammerSkill>(){ programmerSkills[4] }  },
    //            new Skill{ Name = "Front-end", Description = "Creating front of cites" },
    //            new Skill{ Name = "Python", Description = "language", ProgrammerSkills = new List<ProgrammerSkill>(){ programmerSkills[7] }  },
    //            new Skill{ Name = "PHP", Description = "language" },
    //            new Skill{ Name = "GO", Description = "language", ProgrammerSkills = new List<ProgrammerSkill>(){ programmerSkills[6] }  }
    //        };

    //        foreach (Skill skill in skills)
    //            db.Skills.Add(skill);

    //        List <Programmer> programmers = new List<Programmer>
    //        {
    //            new Programmer{ FullName = "Hristich Bogdan", Age = 21, Address = "Kiev", Email = "bullhardyua@gmail.com", GitHub = "hardy4787", Phone="+380671605738",
    //                ProgrammerSkills=new List<ProgrammerSkill>(){
    //                    programmerSkills[0],programmerSkills[1]
    //                },
    //                Educations = new List<Education>(){
    //                    educations[0]
    //                },
    //                Projects = new List<Project>(){
    //                    projects[0]
    //                }
    //            },
    //            new Programmer{ FullName = "Basenko Ivan", Age = 22, Address = "Kiev", Email = "bullhardyua@gmail.com", GitHub = "hardy4787", Phone="+380652805738",
    //                ProgrammerSkills=new List<ProgrammerSkill>(){
    //                    programmerSkills[2],programmerSkills[3],programmerSkills[4]
    //                },
    //                Educations = new List<Education>(){
    //                    educations[1]
    //                },
    //                Projects = new List<Project>(){
    //                    projects[1]
    //                }
    //            },
    //            new Programmer{ FullName = "Stratila Denis", Age = 27, Address = "Kharkov", Email = "Aashardyua@gmail.com", GitHub = "koy42", Phone="+380163205296",
    //                ProgrammerSkills=new List<ProgrammerSkill>(){
    //                    programmerSkills[5]
    //                },
    //                Educations = new List<Education>(){
    //                    educations[2]
    //                },
    //                Projects = new List<Project>(){
    //                    projects[2]
    //                }
    //            },
    //            new Programmer{ FullName = "Dogmash Amir", Age = 19, Address = "Lviv", Email = "sadullhardyua@gmail.com", GitHub = "dfs13213", Phone="+380341605712",
    //                ProgrammerSkills=new List<ProgrammerSkill>(){
    //                    programmerSkills[6],programmerSkills[7]
    //                },
    //                Educations = new List<Education>(){
    //                    educations[3]
    //                },
    //                Projects = new List<Project>(){
    //                    projects[3]
    //                }
    //            },
    //            new Programmer{ FullName = "Cherepanceva Anna", Age = 25, Address = "Rostov", Email = "basdrdyua@gmail.com", GitHub = "dfsf341", Phone="+380675675734",
    //                ProgrammerSkills=new List<ProgrammerSkill>(){

    //                },
    //                Educations = new List<Education>(){
    //                    educations[4]
    //                },
    //                Projects = new List<Project>(){

    //                }
    //            }
    //        };

    //        foreach (Programmer programmer in programmers)
    //            db.Programmers.Add(programmer);

    //        db.SaveChanges();
    //    }
    //}
}
