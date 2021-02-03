using AutoMapper;
using CollegeRoadApplication.Dtos;
using CollegeRoadApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Source and Target
            Mapper.CreateMap<SwimmingMeet, SwimmingMeetDto>();
            Mapper.CreateMap<SwimmingMeetDto, SwimmingMeet>();
        }
    }
}