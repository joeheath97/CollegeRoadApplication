using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CollegeRoadApplication.Controllers;
using CollegeRoadApplication.DAL;
using Moq;
using CollegeRoadApplication.Models;
using System.Linq;

namespace UnitTestCollegeRoadSwimmingClub.DAL
{
    [TestClass]
    public class SwimmingMeetControllerTest
    {
        private Mock<ISwimmingMeetRepository> _mockRepository;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<ISwimmingMeetRepository>();

            var swimmingMeet1 = new SwimmingMeet { Id = 1, Name = "Meet 1", PoolSizeTypeId = 1 };

            //Edit
            _mockRepository.Setup(m => m.GetSwimmingMeetById(1)).Returns(swimmingMeet1);
            
            // Delete
            _mockRepository.Setup(m => m.FindById(1)).Returns(swimmingMeet1);

        }


        // << -------- INDEX -------->>
        [TestMethod]
        public void Test_Swimming_Meet_Index_not_null_View()
        {
            //Arrange
            SwimmingMeetController controller = new SwimmingMeetController(_mockRepository.Object);
            // Act
            var result = controller.Index() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }

        // << -------- NEW -------->>
        [TestMethod]
        public void Test_Swimming_Meet_New_ViewResult()
        {
            //Arrange
            SwimmingMeetController controller = new SwimmingMeetController(_mockRepository.Object);
            // Act
            var result = controller.New() as ViewResult;
            //Assert
            Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

        // << -------- EDIT -------->>
        [TestMethod]
        public void Test_Edit_returns_type_of_HttpNotFoundResult()
        {

            //Arrange
            //Create the controller instance
            SwimmingMeetController controller = new SwimmingMeetController(_mockRepository.Object);

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
            SwimmingMeetController controller = new SwimmingMeetController(_mockRepository.Object);

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
            SwimmingMeetController controller = new SwimmingMeetController(_mockRepository.Object);

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
            SwimmingMeetController controller = new SwimmingMeetController(_mockRepository.Object);

            //Act
            var detailsResult = controller.Delete(1);

            //Assert
            Assert.IsInstanceOfType(detailsResult, typeof(ViewResult));
        }
    }
}
