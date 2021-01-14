using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using CollegeRoadApplication.Controllers;
using CollegeRoadApplication.DAL;
using CollegeRoadApplication.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestCollegeRoadSwimmingClub.DAL
{
    [TestClass]
    public class FamilyGroupControllTest
    {

        private Mock<IFamilyGroupRepository> _mockRepository;
        private FamilyGroup familyGroup1;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IFamilyGroupRepository>();

            familyGroup1 = new FamilyGroup { Id = 1, Name = "Heath's", Members = null};

            //Edit
            _mockRepository.Setup(m => m.GetFamilyGroupById(1)).Returns(familyGroup1);

            // Delete
            _mockRepository.Setup(m => m.FindById(1)).Returns(familyGroup1);

            //Save
            _mockRepository.Setup(x => x.GetFamilyGroupInDb(1)).Returns(familyGroup1);

        }

        // << -------- EDIT -------->>
        [TestMethod]
        public void Test_Edit_returns_type_of_HttpNotFoundResult()
        {

            //Arrange
            //Create the controller instance
            FamilyGroupController controller = new FamilyGroupController(_mockRepository.Object);

            //Act
            var detailsResult = controller.Edit(5);

            //Assert
            Assert.IsInstanceOfType(detailsResult, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Test_Edit_returns_type_of_ViewResult()
        {

            //Arrange
            //Create the controller instance
            FamilyGroupController controller = new FamilyGroupController(_mockRepository.Object);

            //Act
            var detailsResult = controller.Edit(1);

            //Assert
            Assert.IsInstanceOfType(detailsResult, typeof(ViewResult));
        }

        // << -------- DELETE -------->>
        [TestMethod]
        public void Test_Delete_returns_type_of_HttpNotFoundResult()
        {

            //Arrange
            //Create the controller instance
            FamilyGroupController controller = new FamilyGroupController(_mockRepository.Object);

            //Act
            var detailsResult = controller.Delete(5);

            //Assert
            Assert.IsInstanceOfType(detailsResult, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Test_Delete_returns_type_of_ViewResult()
        {

            //Arrange
            //Create the controller instance
            FamilyGroupController controller = new FamilyGroupController(_mockRepository.Object);

            //Act
            var detailsResult = controller.Delete(1);

            //Assert
            Assert.IsInstanceOfType(detailsResult, typeof(ViewResult));
        }

        // << -------- SAVE -------->>

        [TestMethod]
        public void Test_Save_returns_type_of_ViewResult()
        {

            //Arrange
            //Create the controller instance
            FamilyGroupController controller = new FamilyGroupController(_mockRepository.Object);

            //Act
            var detailsResult = controller.Save(familyGroup1);

            //Assert
            Assert.IsInstanceOfType(detailsResult, typeof(RedirectToRouteResult));
        }

    }
}
