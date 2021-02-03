﻿using AutoMapper;
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

            // Domain to Dto
            Mapper.CreateMap<SwimmingMeet, SwimmingMeetDto>();
            Mapper.CreateMap<SwimmingEvent, SwimmingEventDto>();


            // Dto to Domain
            Mapper.CreateMap<SwimmingMeetDto, SwimmingMeet>().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<SwimmingEventDto, SwimmingEvent>().ForMember(c => c.Id, opt => opt.Ignore());

        }
    }
}