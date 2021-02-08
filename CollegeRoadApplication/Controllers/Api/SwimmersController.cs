using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using CollegeRoadApplication.DAL;
using CollegeRoadApplication.Dtos;
using CollegeRoadApplication.Models;

namespace CollegeRoadApplication.Controllers.Api
{
    public class SwimmersController : ApiController
    {
        private readonly ISwimmingEventRepository _swimmingEventRepository;

        public SwimmersController()
        {
            _swimmingEventRepository = new SwimmingEventRepository(new ApplicationDbContext());
        }

        public SwimmersController(ISwimmingEventRepository swimmingEventRepository)
        {
            _swimmingEventRepository = swimmingEventRepository;
        }

        protected override void Dispose(bool disposing)
        {
            _swimmingEventRepository.Dispose();
        }

        // GET: /api/swimmer/userId
        /**
        * Param {id} - Application User Id
        */
        [HttpGet]
        public IHttpActionResult GetSwimmersEvents(string id)
        {
            var lanes = _swimmingEventRepository.GetMemberEventLanes(id);

            var swimmingEvents = new List<SwimmingEvent>();

            foreach (var lane in lanes)
            {
                // should be single and defualt > instead of single 
                swimmingEvents.Add(_swimmingEventRepository.GetSwimmingEventById(lane.SwimmingEventId));

            }

            var swimmingEventDto = swimmingEvents.Select(Mapper.Map<SwimmingEvent, SwimmingEventDto>);

            return Ok(swimmingEventDto);
        }
    }
}
