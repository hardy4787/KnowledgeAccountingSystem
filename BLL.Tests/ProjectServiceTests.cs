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
    public class ProjectServiceTests
    {
        [TestMethod]
        public void InsertProject_NewProjectAddingToDatabase_ShouldBeAddedNewProject()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProjectService service = new ProjectService(uow.Object);
            uow.Setup(a => a.Projects.Get(It.IsAny<int>())).Returns((Project)null);
            service.Insert(new ProjectDTO() { Id = 1, ProgrammerId = "1" });
            uow.Verify(x => x.Save());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void InsertProjectByIdProfile_InvalidProjectId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProjectService service = new ProjectService(uow.Object);
            uow.Setup(a => a.Projects.Get(It.IsAny<int>())).Returns(new Project());
            service.Insert(new ProjectDTO { Id = 1 });
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void InsertProjectByIdProfile_InvalidProjectObject_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProjectService service = new ProjectService(uow.Object);
            service.Insert(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateProjectByIdProfile_InvalidProjectObject_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProjectService service = new ProjectService(uow.Object);
            service.Update(It.IsAny<int>(), null);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateProjectByIdProfile_ProjectIdNotMatch_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProjectService service = new ProjectService(uow.Object);
            service.Update(1, new ProjectDTO() { Id = 2, ProgrammerId = "1" });
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateProjectByIdProfile_InvalidProjectId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProjectService service = new ProjectService(uow.Object);
            uow.Setup(a => a.Projects.Get(It.IsAny<int>())).Returns((Project)null);
            service.Update(2, new ProjectDTO() { Id = 2, ProgrammerId = "1" });
        }
        [TestMethod]
        public void UpdateProject_ProjectExist_ShouldBeEditingSaved()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProjectService service = new ProjectService(uow.Object);
            uow.Setup(a => a.Projects.Get(It.IsAny<int>())).Returns(new Project());
            service.Update(It.IsAny<int>(), new ProjectDTO());
            uow.Verify(x => x.Save());
        }
        [TestMethod]
        public void DeleteProject_DeletedProjectWithCorrectId_ShouldBeDeleted()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProjectService service = new ProjectService(uow.Object);
            uow.Setup(a => a.Projects.Get(It.IsAny<int>())).Returns(new Project());
            service.Delete(It.IsAny<int>());
            uow.Verify(x => x.Save());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void DeleteProject_InvalidProjectId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProjectService service = new ProjectService(uow.Object);
            uow.Setup(a => a.Projects.Get(It.IsAny<int>())).Returns((Project)null);
            service.Delete(It.IsAny<int>());
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void GetProjectByIdProfile_InvalidProfileId_ShouldBeThrownValidationException()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            ProjectService service = new ProjectService(uow.Object);
            uow.Setup(a => a.ProgrammerProfiles.Get(It.IsAny<string>())).Returns((ProgrammerProfile)null);
            service.GetProjectsByProfileId(It.IsAny<string>());
        }

        [TestMethod]
        public void GetProjectByIdProfile_GetProjectWithCorrectProfileId_ShouldBeRecieved()
        {
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            IUnitOfWork uow = mock.Object;
            ProjectService service = new ProjectService(uow);

            IEnumerable<Project> projects = new List<Project>
            {
                new Project() { Id = 1, ProgrammerId = "1" },
                new Project() { Id = 2, ProgrammerId = "1" },
                new Project() { Id = 3, ProgrammerId = "1" }
            };
            var expected = new List<ProjectDTO>
            {
                new ProjectDTO() { Id = 1, ProgrammerId = "1" },
                new ProjectDTO() { Id = 2, ProgrammerId = "1" },
                new ProjectDTO() { Id = 3, ProgrammerId = "1" }
            };
            mock.Setup(a => a.ProgrammerProfiles.Get(It.IsAny<string>())).Returns(new ProgrammerProfile());
            mock.Setup(a => a.Projects.GetAll()).Returns(projects);
            var actual = service.GetProjectsByProfileId("1");
            CollectionAssert.AreEquivalent(actual.Select(x => x.Id).ToList(), expected.Select(x => x.Id).ToList());
            CollectionAssert.AreEquivalent(actual.Select(x => x.ProgrammerId).ToList(), expected.Select(x => x.ProgrammerId).ToList());
        }
    }
}
