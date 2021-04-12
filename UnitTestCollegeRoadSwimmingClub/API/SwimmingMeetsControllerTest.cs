using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using CollegeRoadApplication.App_Start;
using CollegeRoadApplication.Controllers.Api;
using CollegeRoadApplication.DAL;
using CollegeRoadApplication.Dtos;
using CollegeRoadApplication.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestCollegeRoadSwimmingClub.API
{
    [TestClass]
    public class SwimmingMeetsControllerTest
    {
        private Mock<ISwimmingMeetRepository> _mockRepository;
        private SwimmingMeetsController _controller;

        [TestInitialize]
        public void Initialise()
        {
            _mockRepository = new Mock<ISwimmingMeetRepository>();

            Mapper.Initialize(c => c.AddProfile<MappingProfile>());

            // Arrange
            _controller = new SwimmingMeetsController(_mockRepository.Object);

            var model = new SwimmingMeet() { Id = 3 };
            
            var models = new List<SwimmingMeet>()
            {
                new SwimmingMeet() {Id = 1},
                new SwimmingMeet() {Id = 2},
                new SwimmingMeet() {Id = 4},
            };

            // GET by Id
            _mockRepository.Setup(x => x.GetSwimmingMeetById(3)).Returns(model);

            // GET
            _mockRepository.Setup(x => x.GetAllSwimmingMeets()).Returns(models);

        }

        [TestMethod]
        public void Get_All()
        {

            // Act
            var actionResult = _controller.GetSwimmingMeets();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<SwimmingMeetDto>>;

            // Assert
            Assert.AreEqual(3, contentResult.Content.Count());
        }

        [TestMethod]
        public void Create_SwimmingMeet()
        {
            // Act
            IHttpActionResult actionResult = _controller.CreateSwimmingMeet(new SwimmingMeetDto()
            {
                Name = "TEST",
                Date = DateTime.Now,
                PoolSizeTypeId = 1,
                Vanue = "Test",
                
            });

            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<SwimmingMeetDto>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(0, createdResult.RouteValues["id"]);
        }
    }
}
