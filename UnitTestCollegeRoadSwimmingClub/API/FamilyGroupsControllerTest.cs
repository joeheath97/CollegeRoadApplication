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
    public class FamilyGroupsControllerTest
    {
        private Mock<IFamilyGroupRepository> _mockRepository;
        private FamilyGroupsController _controller;

        [TestInitialize]
        public void Initialise()
        {
            _mockRepository = new Mock<IFamilyGroupRepository>();

            Mapper.Initialize(c => c.AddProfile<MappingProfile>());

            // Arrange
            _controller = new FamilyGroupsController(_mockRepository.Object);

            var model = new FamilyGroup() { Id = 3 };
            
            var models = new List<FamilyGroup>()
            {
                new FamilyGroup() {Id = 1},
                new FamilyGroup() {Id = 2},
                new FamilyGroup() {Id = 4},
            };

            // GET by Id
            _mockRepository.Setup(x => x.GetFamilyGroupById(3)).Returns(model);

            // GET
            _mockRepository.Setup(x => x.GetAllFamilyGroups()).Returns(models);

        }

        [TestMethod]
        public void Get_All()
        {

            // Act
            var actionResult = _controller.GetFamilyGroups();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<FamilyGroupDto>>;

            // Assert
            Assert.AreEqual(3, contentResult.Content.Count());
        }

        [TestMethod]
        public void Get_By_Id_Returns_FamilyGroup_With_Same_Id()
        {


            // Act
            var actionResult = _controller.GetUsersFamilyGroup(3);
            var contentResult = actionResult as OkNegotiatedContentResult<FamilyGroupDto>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(3, contentResult.Content.Id);
        }

        [TestMethod]
        public void Get_By_Id_Returns_Not_Found()
        {

            // Act
            var actionResult = _controller.GetUsersFamilyGroup(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

    }
}
