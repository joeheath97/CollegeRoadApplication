using AutoMapper;
using CollegeRoadApplication.DAL;
using CollegeRoadApplication.Models;
using CollegeRoadApplication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CollegeRoadApplication.Controllers.Api
{
    public class MembersController : ApiController
    {
        private IMemberRepository _memberRepository;

        public MembersController()
        {
            _memberRepository = new MemberRepository(new ApplicationDbContext());
        }

        public MembersController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        protected override void Dispose(bool disposing)
        {
            _memberRepository.Dispose();
        }


        // GET: /api/members
        public IHttpActionResult GetMembers()
        {
            var MemberDto = _memberRepository.GetAllMembers().Select(Mapper.Map<ApplicationUser, MemberDto>);

            return Ok(MemberDto);
        }


        // PUT: /api/member/5
        [HttpPut]
        public IHttpActionResult UpdateMember(string id, MemberDto memberDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var memberInDB = _memberRepository.GetMemberInDB(id);

            if (memberDto == null)
            {
                return NotFound();
            }

            Mapper.Map(memberDto, memberInDB);

            _memberRepository.Save();

            return Ok();
        }

        
    }
}
