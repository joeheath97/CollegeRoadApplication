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
    public class SwimmingMeetsController : ApiController
    {

        private readonly ISwimmingMeetRepository _swimmingMeetRepository;

        public SwimmingMeetsController()
        {
            _swimmingMeetRepository = new SwimmingMeetRepository(new ApplicationDbContext());
        }

        public SwimmingMeetsController(ISwimmingMeetRepository swimmingMeetRepository)
        {
            _swimmingMeetRepository = swimmingMeetRepository;
        }

        protected override void Dispose(bool disposing)
        {
            _swimmingMeetRepository.Dispose();
        }


        // GET: /api/SwimmingMeets
        public IHttpActionResult GetSwimmingMeets()
        {
            var swimmingMeetDto = _swimmingMeetRepository.GetAllSwimmingMeets().Select(Mapper.Map<SwimmingMeet, SwimmingMeetDto>);

            return Ok(swimmingMeetDto);
        }

        // POST: /api/SwimmingMeets/5
        [HttpPost]
        [Authorize(Roles = "Admin,SCO")]
        public IHttpActionResult CreateSwimmingMeet(SwimmingMeetDto swimmingMeetDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var swimmingMeet = Mapper.Map<SwimmingMeetDto, SwimmingMeet>(swimmingMeetDto);

            _swimmingMeetRepository.Add(swimmingMeet);
            _swimmingMeetRepository.Save();

            // The database has asigneed the new customer an id so it needs to be set to the SwimmerDto
            swimmingMeetDto.Id = swimmingMeet.Id;

            return CreatedAtRoute("DefaultApi", new { id = swimmingMeetDto.Id }, swimmingMeetDto);
        }
    }
}
