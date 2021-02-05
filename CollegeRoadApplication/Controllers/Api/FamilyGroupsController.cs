using AutoMapper;
using CollegeRoadApplication.DAL;
using CollegeRoadApplication.Dtos;
using CollegeRoadApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CollegeRoadApplication.Controllers.Api
{
    public class FamilyGroupsController : ApiController
    {
        private readonly IFamilyGroupRepository _familyGroupRepository;

        public FamilyGroupsController()
        {
            _familyGroupRepository = new FamilyGroupRepository(new ApplicationDbContext());
        }

        public FamilyGroupsController(IFamilyGroupRepository familyGroupRepository)
        {
            _familyGroupRepository = familyGroupRepository;
        }

        protected override void Dispose(bool disposing)
        {
            // this release the connection 
            _familyGroupRepository.Dispose();
        }

        // GET: /api/familygroup
        public IHttpActionResult GetFamilyGroups()
        {
            var familyGroupDto = _familyGroupRepository.GetAllFamilyGroups().Select(Mapper.Map<FamilyGroup, FamilyGroupDto>);

            return Ok(familyGroupDto);
        }

        // POST: /api/familygroup
        [HttpPost]
        [Authorize(Roles = "Admin,SCO")]
        public IHttpActionResult CreateFamilyGroup(FamilyGroupDto familyGroupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var familyGroup = Mapper.Map<FamilyGroupDto, FamilyGroup>(familyGroupDto);

            _familyGroupRepository.Add(familyGroup);
            _familyGroupRepository.Save();

            familyGroupDto.Id = familyGroup.Id;

            return CreatedAtRoute("DefaultApi", new {id = familyGroupDto.Id}, familyGroupDto);
        }

    }
}
