using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Interfaces;
using BLL.Services;
using DAL.Entities;
using BLL.Infrastructure;
using BLL.DTO;
using Util;
using System.Linq;
using System.Collections.Generic;

namespace BLL.Tests
{
    [TestClass]
    public class ProgrammerProfileServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateProfileById_InvalidProfileObject_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProgrammerProfileService service = new ProgrammerProfileService(uow.Object);
            service.Update(It.IsAny<string>(), null);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateProfileById_InvalidProfileId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProgrammerProfileService service = new ProgrammerProfileService(uow.Object);
            service.Update("1", new ProgrammerProfileDTO() { Id = "2" });
        }
        [TestMethod]
        public void UpdateProfileById_ProfileExist_ShouldBeEditingSaved()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProgrammerProfileService service = new ProgrammerProfileService(uow.Object);
            uow.Setup(a => a.ProgrammerProfiles.Get(It.IsAny<string>())).Returns(new ProgrammerProfile());
            service.Update(It.IsAny<string>(), new ProgrammerProfileDTO());
            uow.Verify(x => x.Save());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GetProfileById_InvalidProfileId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProgrammerProfileService service = new ProgrammerProfileService(uow.Object);
            uow.Setup(a => a.ProgrammerProfiles.Get(It.IsAny<string>())).Returns((ProgrammerProfile)null);
            service.Get(It.IsAny<string>());
        }
        [TestMethod]
        public void GetProfileById_ProfileExist_ShouldBeRecieved()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProgrammerProfileService service = new ProgrammerProfileService(uow.Object);
            uow.Setup(a => a.ProgrammerProfiles.Get(It.IsAny<string>())).Returns(new ProgrammerProfile());
            service.Get(It.IsAny<string>());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GetProgrammersBySkill_InvalidKnowledgeLevel_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProgrammerProfileService service = new ProgrammerProfileService(uow.Object);
            service.GetProgrammersBySkill(null, 120);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GetProgrammersBySkill_InvalidSkillId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProgrammerProfileService service = new ProgrammerProfileService(uow.Object);
            uow.Setup(a => a.Skills.Get(It.IsAny<int>())).Returns((Skill)null);
            service.GetProgrammersBySkill(It.IsAny<int>(), 100);
        }
        [TestMethod]
        public void GetProgrammersBySkill_GetProgrammersBySkillIdNullAndKnowledgeLevel50_ShouldBeRecieved()
        {
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            IUnitOfWork uow = mock.Object;
            ProgrammerProfileService service = new ProgrammerProfileService(uow);

            IEnumerable<ProgrammerSkill> programmerSkills = new List<ProgrammerSkill>
            {
                new ProgrammerSkill() { ProgrammerId = "1", SkillId = 1, KnowledgeLevel = 20 },
                new ProgrammerSkill() { ProgrammerId = "2", SkillId = 2, KnowledgeLevel = 60 },
                new ProgrammerSkill() { ProgrammerId = "3", SkillId = 1, KnowledgeLevel = 70 }
            };
            IEnumerable<ProgrammerProfile> profiles = new List<ProgrammerProfile>
            {
                new ProgrammerProfile() { Id ="1" },
                new ProgrammerProfile() { Id ="2" },
                new ProgrammerProfile() { Id ="3" }
            };
            var expected = new List<ProgrammerProfileDTO>
            {
                new ProgrammerProfileDTO() { Id = "2"},
                new ProgrammerProfileDTO() { Id = "3"}
            };
            mock.Setup(a => a.ProgrammerProfiles.GetAll()).Returns(profiles);
            mock.Setup(a => a.ProgrammerSkills.GetAll()).Returns(programmerSkills);
            var actual = service.GetProgrammersBySkill(null, 50);
            CollectionAssert.AreEquivalent(actual.Select(x => x.Id).ToList(), expected.Select(x => x.Id).ToList());
        }
        [TestMethod]
        public void GetProgrammersBySkill_GetProgrammersBySkillId2Level50_ShouldBeRecieved()
        {
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            IUnitOfWork uow = mock.Object;
            ProgrammerProfileService service = new ProgrammerProfileService(uow);

            IEnumerable<ProgrammerSkill> programmerSkills = new List<ProgrammerSkill>
            {
                new ProgrammerSkill() { ProgrammerId = "1", SkillId = 1, KnowledgeLevel = 20 },
                new ProgrammerSkill() { ProgrammerId = "2", SkillId = 2, KnowledgeLevel = 60 },
                new ProgrammerSkill() { ProgrammerId = "3", SkillId = 1, KnowledgeLevel = 70 }
            };
            IEnumerable<ProgrammerProfile> profiles = new List<ProgrammerProfile>
            {
                new ProgrammerProfile() { Id ="1" },
                new ProgrammerProfile() { Id ="2" },
                new ProgrammerProfile() { Id ="3" }
            };
            var expected = new List<ProgrammerProfileDTO>
            {
                new ProgrammerProfileDTO() { Id = "2"}
            };
            mock.Setup(a => a.ProgrammerProfiles.GetAll()).Returns(profiles);
            mock.Setup(a => a.Skills.Get(It.IsAny<int>())).Returns(new Skill());
            mock.Setup(a => a.ProgrammerSkills.GetAll()).Returns(programmerSkills);
            var actual = service.GetProgrammersBySkill(2, 50);
            CollectionAssert.AreEquivalent(actual.Select(x => x.Id).ToList(), expected.Select(x => x.Id).ToList());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateImageProfileUrl_InvalidFileType_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProgrammerProfileService service = new ProgrammerProfileService(uow.Object);
            service.UpdateImageProfileUrl(It.IsAny<string>(), ".html", It.IsAny<int>(), It.IsAny<string>());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateImageProfileUrl_InvalidFileSize_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProgrammerProfileService service = new ProgrammerProfileService(uow.Object);
            service.UpdateImageProfileUrl(It.IsAny<string>(), ".jpeg", 1024 * 1024 * 3, It.IsAny<string>());
        }
        [TestMethod]
        public void UpdateImageProfileUrl_UpdateImageProfileUrlWithCorrectData_ShouldBeUpdated()
        {
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            IUnitOfWork uow = mock.Object;
            ProgrammerProfileService service = new ProgrammerProfileService(uow);
            mock.Setup(a => a.ProgrammerProfiles.Get(It.IsAny<string>())).Returns(new ProgrammerProfile());
            service.UpdateImageProfileUrl("/assets/image-profiles/", ".jpeg", 1024 * 1024 * 2, It.IsAny<string>());
            mock.Verify(x => x.Save());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GenerateReport_EmptyProfileList_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            IUnitOfWork uow = mock.Object;
            ProgrammerProfileService service = new ProgrammerProfileService(uow);
            service.GenerateReport(new List<ProgrammerProfileDTO>() { });
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GenerateReport_ProfileListNull_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            IUnitOfWork uow = mock.Object;
            ProgrammerProfileService service = new ProgrammerProfileService(uow);
            service.GenerateReport(null);
        }
        //[TestMethod]
        //[ExpectedException(typeof(ValidationException))]
        //public void GenerateReport_ProfileListValid_ShouldBeReturnedByteArray()
        //{
        //    Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
        //    IUnitOfWork uow = mock.Object;
        //    var profiles = new List<ProgrammerProfileDTO>()
        //    {
        //        new ProgrammerProfileDTO()
        //        {
        //            Id = "1", Address = "Kiev", Age=21, Email="djboda972@mail.ru", FullName = "Bogdan"
        //        },
        //        new ProgrammerProfileDTO()
        //        {
        //            Id = "2", Address = "Kharkiv", Age=25, Email="kek@mail.ru", FullName = "Ivan"
        //        }
        //    };
        //    ProgrammerProfileService service = new ProgrammerProfileService(uow);
        //    service.GenerateReport(profiles);
        //}
    }
}
