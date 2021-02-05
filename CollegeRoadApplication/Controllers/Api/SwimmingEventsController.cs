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
    public class SwimmingEventsController : ApiController
    {
        private readonly ISwimmingEventRepository _swimmingEventRepository;

        public SwimmingEventsController()
        {
            _swimmingEventRepository = new SwimmingEventRepository(new ApplicationDbContext());
        }

        public SwimmingEventsController(ISwimmingEventRepository swimmingEventRepository)
        {
            _swimmingEventRepository = swimmingEventRepository;
        }

        protected override void Dispose(bool disposing)
        {
            _swimmingEventRepository.Dispose();
        }

   
        // GET: /api/swimmingEvents 
        public IHttpActionResult GetSwimmingEvents()
        {

            var swimmingEventDto = _swimmingEventRepository.GetAllSwimmingEvents().Select(Mapper.Map<SwimmingEvent, SwimmingEventDto>);

            return Ok(swimmingEventDto);
        }

        // POST: /api/swimmingEvents/
        [HttpPost]
        [Authorize(Roles = "Admin,SCO")]
        public IHttpActionResult CreateSwimmingEvent(SwimmingEventDto swimmingEventDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var swimmingEvent = Mapper.Map<SwimmingEventDto, SwimmingEvent>(swimmingEventDto);

            _swimmingEventRepository.Add(swimmingEvent);
            _swimmingEventRepository.Save();

            swimmingEventDto.Id = swimmingEvent.Id;

            return CreatedAtRoute("DefaultApi", new { id = swimmingEventDto.Id }, swimmingEventDto);
        }
    }
}
