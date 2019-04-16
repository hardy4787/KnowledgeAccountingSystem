using BLL.DTO;
using BLL.Infrastructure;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Tests
{
    [TestClass]
    public class SkillServiceTests
    {
        [TestMethod]
        public void InsertSkill_NewSkillAddingToDatabase_ShouldBeAddedNewEducation()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            uow.Setup(a => a.Skills.Get(It.IsAny<int>())).Returns((Skill)null);
            service.Insert(new SkillDTO() { Id = 1 });
            uow.Verify(x => x.Save());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void InsertSkillByIdProfile_InvalidSkillObject_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            service.Insert(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void InsertSkill_ThisSkillNameAlreadyExist_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            var skills = new List<Skill>()
            {
                new Skill()
                {
                    Id = 1,
                    Name = "C#"
                }
            };
            SkillDTO skillDTO = new SkillDTO
            {
                Id = 2,
                Name = "C#"
            };
            uow.Setup(a => a.Skills.GetAll()).Returns(skills);
            service.Insert(skillDTO);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void InsertSkill_SkillWhisThisIdAlreadyExist_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            var skills = new List<Skill>()
            {
                new Skill()
                {
                    Id = 1,
                    Name = "Java"
                }
            };
            SkillDTO skillDTO = new SkillDTO
            {
                Id = 1,
                Name = "C#"
            };
            uow.Setup(a => a.Skills.GetAll()).Returns(skills);
            service.Insert(skillDTO);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateSkillByIdProfile_InvalidSkillObject_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            service.Update(It.IsAny<int>(), null);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateSkillByIdProfile_SkillIdNotMatch_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            service.Update(1, new SkillDTO() { Id = 2 });
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateWorkExperienceByIdProfile_InvalidWorkExperienceId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            uow.Setup(a => a.Skills.Get(It.IsAny<int>())).Returns((Skill)null);
            service.Update(2, new SkillDTO() { Id = 2 });
        }
        [TestMethod]
        public void UpdateSkill_SkillExist_ShouldBeEditingSaved()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            uow.Setup(a => a.Skills.Get(It.IsAny<int>())).Returns(new Skill());
            service.Update(It.IsAny<int>(), new SkillDTO());
            uow.Verify(x => x.Save());
        }
        [TestMethod]
        public void DeleteSkill_DeletedSkillWithCorrectId_ShouldBeDeleted()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            uow.Setup(a => a.Skills.Get(It.IsAny<int>())).Returns(new Skill());
            service.Delete(It.IsAny<int>());
            uow.Verify(x => x.Save());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void DeleteSkill_InvalidSkillId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            uow.Setup(a => a.Skills.Get(It.IsAny<int>())).Returns((Skill)null);
            service.Delete(It.IsAny<int>());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateSkillOfProgrammer_SkillIdNotMatch_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            service.UpdateSkillOfProgrammer(1, new ProgrammerSkillDTO() { SkillId = 2, ProgrammerId = "1" });
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateSkillOfProgrammer_ProgrammerSkillNotExist_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            uow.Setup(a => a.ProgrammerSkills.Get(It.IsAny<string>(), It.IsAny<int>())).Returns((ProgrammerSkill)null);
            service.UpdateSkillOfProgrammer(1, new ProgrammerSkillDTO() { SkillId = 1, ProgrammerId = "1" });
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateSkillOfProgrammer_ProgrammerSkillObjectInvalid_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            service.UpdateSkillOfProgrammer(1, null);
        }
        [TestMethod]
        public void UpdateSkillOfProgrammer_ProgrammerSkillExist_ShouldBeUpdated()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            uow.Setup(a => a.ProgrammerSkills.Get(It.IsAny<string>(), It.IsAny<int>())).Returns(new ProgrammerSkill());
            service.UpdateSkillOfProgrammer(1, new ProgrammerSkillDTO() { SkillId = 1, ProgrammerId = "1" });
            uow.Verify(x => x.Save());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void InsertSkillToProgrammer_InvalidProgrammerSkillObject_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            service.InsertSkillToProgrammer(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void InsertSkillToProgrammer_ProgrammerSkillAlreadyExist_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            uow.Setup(a => a.ProgrammerSkills.Get(It.IsAny<string>(), It.IsAny<int>())).Returns(new ProgrammerSkill());
            service.InsertSkillToProgrammer(new ProgrammerSkillDTO());
        }
        [TestMethod]
        public void InsertSkillToProgrammer_NewProgrammerAddToDatabase_ShouldBeAddedNewProgrammerSkill()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            uow.Setup(a => a.ProgrammerSkills.Get(It.IsAny<string>(), It.IsAny<int>())).Returns((ProgrammerSkill)null);
            service.InsertSkillToProgrammer(new ProgrammerSkillDTO() { SkillId = 1, ProgrammerId = "1" });
            uow.Verify(x => x.Save());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void DeleteSkillOfProgrammer_SkillNotExist_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            uow.Setup(a => a.Skills.Get(It.IsAny<int>())).Returns((Skill)null);
            service.DeleteSkillOfProgrammer(It.IsAny<string>(), It.IsAny<int>());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void DeleteSkillOfProgrammer_ProgrammerNotHaveThisSkill_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            uow.Setup(a => a.Skills.Get(It.IsAny<int>())).Returns(new Skill());
            uow.Setup(a => a.ProgrammerSkills.Get(It.IsAny<string>(), It.IsAny<int>())).Returns((ProgrammerSkill)null);
            service.DeleteSkillOfProgrammer(It.IsAny<string>(), It.IsAny<int>());
        }
        [TestMethod]
        public void DeleteSkillOfProgrammer_DeleteProgrammerSkill_ShouldBeDeletedProgrammerSkill()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            uow.Setup(a => a.Skills.Get(It.IsAny<int>())).Returns(new Skill());
            uow.Setup(a => a.ProgrammerSkills.Get(It.IsAny<string>(), It.IsAny<int>())).Returns(new ProgrammerSkill());
            service.DeleteSkillOfProgrammer(It.IsAny<string>(), It.IsAny<int>());
            uow.Verify(x => x.Save());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GetSkillsOfProgrammer_InvalidProfileId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            uow.Setup(a => a.ProgrammerProfiles.Get(It.IsAny<string>())).Returns((ProgrammerProfile)null);
            service.GetSkillsOfProgrammer(It.IsAny<string>());
        }
        [TestMethod]
        public void GetSkillsOfProgrammer_GetSkillsWithCorrectProfileId_ShouldBeRecieved()
        {
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            IUnitOfWork uow = mock.Object;
            SkillService service = new SkillService(uow);

            IEnumerable<ProgrammerSkill> programmerSkills = new List<ProgrammerSkill>
            {
                new ProgrammerSkill() { SkillId = 1, ProgrammerId = "1" },
                new ProgrammerSkill() { SkillId = 2, ProgrammerId = "1" },
                new ProgrammerSkill() { SkillId = 3, ProgrammerId = "1" }
            };
            var expected = new List<ProgrammerSkillDTO>
            {
                new ProgrammerSkillDTO() { SkillId = 1, ProgrammerId = "1" },
                new ProgrammerSkillDTO() { SkillId = 2, ProgrammerId = "1" },
                new ProgrammerSkillDTO() { SkillId = 3, ProgrammerId = "1" }
            };
            mock.Setup(a => a.ProgrammerProfiles.Get(It.IsAny<string>())).Returns(new ProgrammerProfile());
            mock.Setup(a => a.ProgrammerSkills.GetAll()).Returns(programmerSkills);
            var actual = service.GetSkillsOfProgrammer("1");
            CollectionAssert.AreEquivalent(actual.Select(x => x.SkillId).ToList(), expected.Select(x => x.SkillId).ToList());
            CollectionAssert.AreEquivalent(actual.Select(x => x.ProgrammerId).ToList(), expected.Select(x => x.ProgrammerId).ToList());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GetSkillsThatTheProgrammerDoesNotHave_InvalidProfileId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            SkillService service = new SkillService(uow.Object);
            uow.Setup(a => a.ProgrammerProfiles.Get(It.IsAny<string>())).Returns((ProgrammerProfile)null);
            service.GetSkillsThatTheProgrammerDoesNotHave(It.IsAny<string>());
        }

        [TestMethod]
        public void GetSkillsThatTheProgrammerDoesNotHave_GetSkillsWithCorrectProfileId_ShouldBeRecieved()
        {
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            IUnitOfWork uow = mock.Object;
            SkillService service = new SkillService(uow);

            IEnumerable<Skill> skills = new List<Skill>
            {
                new Skill() { Id = 1, Name = "C#" },
                new Skill() { Id = 2, Name = "Java" },
                new Skill() { Id = 3, Name = "PHP" }
            };
            IEnumerable<ProgrammerSkill> programmerSkills = new List<ProgrammerSkill>
            {
                new ProgrammerSkill() { SkillId = 1, ProgrammerId = "1" }
            };
            var expected = new List<Skill>
            {
                new Skill() { Id = 2, Name = "Java" },
                new Skill() { Id = 3, Name = "PHP" }
            };
            mock.Setup(a => a.ProgrammerProfiles.Get(It.IsAny<string>())).Returns(new ProgrammerProfile());
            mock.Setup(a => a.ProgrammerSkills.GetAll()).Returns(programmerSkills);
            mock.Setup(a => a.Skills.GetAll()).Returns(skills);
            var actual = service.GetSkillsThatTheProgrammerDoesNotHave("1");
            CollectionAssert.AreEquivalent(actual.Select(x => x.Id).ToList(), expected.Select(x => x.Id).ToList());
            CollectionAssert.AreEquivalent(actual.Select(x => x.Name).ToList(), expected.Select(x => x.Name).ToList());
        }
        [TestMethod]
        public void GetSkillsThatTheProgrammerDoesNotHave_GetSkillsWhenProgrammerDoesNotHaveAnySkiss_ShouldBeRecieved()
        {
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            IUnitOfWork uow = mock.Object;
            SkillService service = new SkillService(uow);

            IEnumerable<Skill> skills = new List<Skill>
            {
                new Skill() { Id = 1, Name = "C#" },
                new Skill() { Id = 2, Name = "Java" },
                new Skill() { Id = 3, Name = "PHP" }
            };
            IEnumerable<ProgrammerSkill> programmerSkills = new List<ProgrammerSkill>();
            var expected = new List<Skill>
            {
                new Skill() { Id = 1, Name = "C#" },
                new Skill() { Id = 2, Name = "Java" },
                new Skill() { Id = 3, Name = "PHP" }
            };
            mock.Setup(a => a.ProgrammerProfiles.Get(It.IsAny<string>())).Returns(new ProgrammerProfile());
            mock.Setup(a => a.ProgrammerSkills.GetAll()).Returns(programmerSkills);
            mock.Setup(a => a.Skills.GetAll()).Returns(skills);
            var actual = service.GetSkillsThatTheProgrammerDoesNotHave("1");
            CollectionAssert.AreEquivalent(actual.Select(x => x.Id).ToList(), expected.Select(x => x.Id).ToList());
            CollectionAssert.AreEquivalent(actual.Select(x => x.Name).ToList(), expected.Select(x => x.Name).ToList());
        }
    }
}
