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
    public class EducationServiceTests
    {
        [TestMethod]
        public void InsertEducation_NewEducationAddingToDatabase_ShouldBeAddedNewEducation()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            EducationService service = new EducationService(uow.Object);
            uow.Setup(a => a.Educations.Get(It.IsAny<int>())).Returns((Education)null);
            service.Insert(new EducationDTO() { Id = 1, ProgrammerId = "1", CloseDate = new DateTime(2010, 11, 10), EntryDate = new DateTime(2009, 10, 10), Level = "high", NameInstitution = "KPI" });
            uow.Verify(x => x.Save());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void InsertEducationByIdProfile_InvalidEducationId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            EducationService service = new EducationService(uow.Object);
            uow.Setup(a => a.Educations.Get(It.IsAny<int>())).Returns(new Education());
            service.Insert(new EducationDTO { Id = 1 });
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void InsertEducationByIdProfile_InvalidEducationObject_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            EducationService service = new EducationService(uow.Object);
            service.Insert(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateEducationByIdProfile_InvalidEducationObject_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            EducationService service = new EducationService(uow.Object);
            service.Update(It.IsAny<int>(), null);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateEducationByIdProfile_EducationIdNotMatch_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            EducationService service = new EducationService(uow.Object);
            service.Update(1, new EducationDTO() { Id = 2, ProgrammerId = "1", CloseDate = new DateTime(2010, 11, 10), EntryDate = new DateTime(2009, 10, 10), Level = "high", NameInstitution = "KPI" });
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateEducationByIdProfile_InvalidEducationId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            EducationService service = new EducationService(uow.Object);
            uow.Setup(a => a.Educations.Get(It.IsAny<int>())).Returns((Education)null);
            service.Update(2, new EducationDTO(){ Id = 2, ProgrammerId = "1", CloseDate = new DateTime(2010, 11, 10), EntryDate = new DateTime(2009, 10, 10), Level = "high", NameInstitution = "KPI" });
        }
        [TestMethod]
        public void UpdateEducation_EducationExist_ShouldBeEditingSaved()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            EducationService service = new EducationService(uow.Object);
            uow.Setup(a => a.Educations.Get(It.IsAny<int>())).Returns(new Education());
            service.Update(It.IsAny<int>(), new EducationDTO());
            uow.Verify(x => x.Save());
        }
        [TestMethod]
        public void DeleteEducation_DeletedEducationWithCorrectId_ShouldBeDeleted()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            EducationService service = new EducationService(uow.Object);
            uow.Setup(a => a.Educations.Get(It.IsAny<int>())).Returns(new Education());
            service.Delete(It.IsAny<int>());
            uow.Verify(x => x.Save());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void DeleteEducation_InvalidEducationId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            EducationService service = new EducationService(uow.Object);
            uow.Setup(a => a.Educations.Get(It.IsAny<int>())).Returns((Education)null);
            service.Delete(It.IsAny<int>());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GetEducationByIdProfile_InvalidProfileId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            EducationService service = new EducationService(uow.Object);
            uow.Setup(a => a.ProgrammerProfiles.Get(It.IsAny<string>())).Returns((ProgrammerProfile)null);
            service.GetEducationByProfileId(It.IsAny<string>());
        }

        [TestMethod]
        public void GetEducationByIdProfile_GetEducationWithCorrectProfileId_ShouldBeRecieved()
        {
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            IUnitOfWork uow = mock.Object;
            EducationService service = new EducationService(uow);

            IEnumerable<Education> educations = new List<Education>
            {
                new Education() { Id = 1, ProgrammerId = "1" },
                new Education() { Id = 2, ProgrammerId = "1" },
                new Education() { Id = 3, ProgrammerId = "1" }
            };
            var expected = new List<EducationDTO>
            {
                new EducationDTO() { Id = 1, ProgrammerId = "1" },
                new EducationDTO() { Id = 2, ProgrammerId = "1" },
                new EducationDTO() { Id = 3, ProgrammerId = "1" }
            };
            mock.Setup(a => a.ProgrammerProfiles.Get(It.IsAny<string>())).Returns(new ProgrammerProfile());
            mock.Setup(a => a.Educations.GetAll()).Returns(educations);
            var actual = service.GetEducationByProfileId("1");
            CollectionAssert.AreEquivalent(actual.Select(x => x.Id).ToList(), expected.Select(x => x.Id).ToList());
            CollectionAssert.AreEquivalent(actual.Select(x => x.ProgrammerId).ToList(), expected.Select(x => x.ProgrammerId).ToList());
        }
    }
}
