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
using System.Text;
using System.Threading.Tasks;

namespace BLL.Tests
{
    [TestClass]
    public class WorkExperienceServiceTests
    {
        [TestMethod]
        public void InsertWorkExperience_NewWorkExperienceAddingToDatabase_ShouldBeAddedNewWorkExperience()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            WorkExperienceService service = new WorkExperienceService(uow.Object);
            uow.Setup(a => a.WorkExperiences.Get(It.IsAny<int>())).Returns((WorkExperience)null);
            service.Insert(new WorkExperienceDTO() { Id = 1, ProgrammerId = "1" });
            uow.Verify(x => x.Save());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void InsertWorkExperienceByIdProfile_InvalidWorkExperienceId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            WorkExperienceService service = new WorkExperienceService(uow.Object);
            uow.Setup(a => a.WorkExperiences.Get(It.IsAny<int>())).Returns(new WorkExperience());
            service.Insert(new WorkExperienceDTO { Id = 1 });
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void InsertWorkExperienceByIdProfile_InvalidWorkExperienceObject_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            WorkExperienceService service = new WorkExperienceService(uow.Object);
            service.Insert(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateWorkExperienceByIdProfile_InvalidWorkExperienceObject_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            WorkExperienceService service = new WorkExperienceService(uow.Object);
            service.Update(It.IsAny<int>(), null);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateWorkExperienceByIdProfile_WorkExperienceIdNotMatch_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            WorkExperienceService service = new WorkExperienceService(uow.Object);
            service.Update(1, new WorkExperienceDTO() { Id = 2, ProgrammerId = "1" });
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateWorkExperienceByIdProfile_InvalidWorkExperienceId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            WorkExperienceService service = new WorkExperienceService(uow.Object);
            uow.Setup(a => a.WorkExperiences.Get(It.IsAny<int>())).Returns((WorkExperience)null);
            service.Update(2, new WorkExperienceDTO() { Id = 2, ProgrammerId = "1"});
        }
        [TestMethod]
        public void UpdateWorkExperience_WorkExperienceExist_ShouldBeEditingSaved()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            WorkExperienceService service = new WorkExperienceService(uow.Object);
            uow.Setup(a => a.WorkExperiences.Get(It.IsAny<int>())).Returns(new WorkExperience());
            service.Update(It.IsAny<int>(), new WorkExperienceDTO());
            uow.Verify(x => x.Save());
        }
        [TestMethod]
        public void DeleteWorkExperience_DeletedWorkExperienceWithCorrectId_ShouldBeDeleted()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            WorkExperienceService service = new WorkExperienceService(uow.Object);
            uow.Setup(a => a.WorkExperiences.Get(It.IsAny<int>())).Returns(new WorkExperience());
            service.Delete(It.IsAny<int>());
            uow.Verify(x => x.Save());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void DeleteWorkExperience_InvalidWorkExperienceId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            WorkExperienceService service = new WorkExperienceService(uow.Object);
            uow.Setup(a => a.WorkExperiences.Get(It.IsAny<int>())).Returns((WorkExperience)null);
            service.Delete(It.IsAny<int>());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GetWorkExperienceByIdProfile_InvalidProfileId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            WorkExperienceService service = new WorkExperienceService(uow.Object);
            uow.Setup(a => a.ProgrammerProfiles.Get(It.IsAny<string>())).Returns((ProgrammerProfile)null);
            service.GetWorkExperienceByProfileId(It.IsAny<string>());
        }

        [TestMethod]
        public void GetWorkExperienceByIdProfile_GetWorkExperienceWithCorrectProfileId_ShouldBeRecieved()
        {
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            IUnitOfWork uow = mock.Object;
            WorkExperienceService service = new WorkExperienceService(uow);

            IEnumerable<WorkExperience> workExperiences = new List<WorkExperience>
            {
                new WorkExperience() { Id = 1, ProgrammerId = "1" },
                new WorkExperience() { Id = 2, ProgrammerId = "1" },
                new WorkExperience() { Id = 3, ProgrammerId = "1" }
            };
            var expected = new List<WorkExperienceDTO>
            {
                new WorkExperienceDTO() { Id = 1, ProgrammerId = "1" },
                new WorkExperienceDTO() { Id = 2, ProgrammerId = "1" },
                new WorkExperienceDTO() { Id = 3, ProgrammerId = "1" }
            };
            mock.Setup(a => a.ProgrammerProfiles.Get(It.IsAny<string>())).Returns(new ProgrammerProfile());
            mock.Setup(a => a.WorkExperiences.GetAll()).Returns(workExperiences);
            var actual = service.GetWorkExperienceByProfileId("1");
            CollectionAssert.AreEquivalent(actual.Select(x => x.Id).ToList(), expected.Select(x => x.Id).ToList());
            CollectionAssert.AreEquivalent(actual.Select(x => x.ProgrammerId).ToList(), expected.Select(x => x.ProgrammerId).ToList());
        }
    }
}
