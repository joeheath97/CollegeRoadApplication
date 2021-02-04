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
        private IFamilyGroupRepository _familyGroupRepository;

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

    }
}
