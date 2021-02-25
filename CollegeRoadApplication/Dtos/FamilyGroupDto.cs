﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.Dtos
{
    public class FamilyGroupDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string  Contact { get; set; }

        public ICollection<MemberDto> Members { get; set; }

    }
}