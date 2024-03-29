﻿using AutoMapper;
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
    public class LanesController : ApiController
    {
        private readonly ILaneRepository _laneRepository;

        public LanesController()
        {
            _laneRepository = new LaneRepository(new ApplicationDbContext());
        }

        public LanesController(ILaneRepository laneRepository)
        {
            _laneRepository = laneRepository;
        }

        protected override void Dispose(bool disposing)
        {
            _laneRepository.Dispose();
        }

        // GET: /api/lanes
        public IHttpActionResult GetLanes()
        {
            var lanesDto = _laneRepository.GetAllLanes().Select(Mapper.Map<Lane, LaneDto>);

            return Ok(lanesDto);
        }

        // POST: /api/lanes
        [HttpPost]
        [Authorize(Roles = "Admin,SCO")]
        public IHttpActionResult CreateLane(LaneDto laneDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var lane = Mapper.Map<LaneDto, Lane>(laneDto); 

            _laneRepository.Add(lane);
            _laneRepository.Save();

            laneDto.Id = lane.Id;

            return CreatedAtRoute("DefaultApi", new { id = laneDto.Id }, laneDto);

        }

        // PUT: /api/lanes/5
        [HttpPut]
        [Authorize(Roles = "Admin,SCO")]
        public IHttpActionResult UpdateLane(int id, LaneDto laneDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var laneInDb = _laneRepository.GetLaneInDb(id);

            if (laneInDb == null)
            {
                return NotFound();
            }

            Mapper.Map(laneDto, laneInDb);

            _laneRepository.Save();

            laneDto.Id = id;

            return Ok(laneDto);
        }
    }
}
