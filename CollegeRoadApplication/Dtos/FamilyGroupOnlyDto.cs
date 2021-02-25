using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.Dtos
{
    public class FamilyGroupOnlyDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string  Contact { get; set; }

    }
}