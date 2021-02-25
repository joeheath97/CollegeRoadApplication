using AutoMapper;
using CollegeRoadApplication.DAL;
using CollegeRoadApplication.Dtos;
using CollegeRoadApplication.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CollegeRoadApplication.Controllers.Api
{
    [RoutePrefix("api/swimmingevents")]
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

        //// GET: /api/swimingEvents/5
        ///**
        //* Param {id} - Swimming Meet Id 
        //*/
        //[HttpGet]
        //public IHttpActionResult GetSwimmingMeetEvents(int id)
        //{
        //    var swimmingEventDto = _swimmingEventRepository.GetAllMeetEvents(id).Select(Mapper.Map<SwimmingEvent, SwimmingEventDto>);

        //    return Ok(swimmingEventDto);
        //}

        [HttpGet]
        [Route("search")]
        public IHttpActionResult Search(string stroke)
        {
            var swimmingEventDto = _swimmingEventRepository.GetAllSwimmingEvents()
                .Where(x => x.Stroke.ToLower().Contains(stroke.ToLower()))
                .Select(Mapper.Map<SwimmingEvent, SwimmingEventDto>);

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
