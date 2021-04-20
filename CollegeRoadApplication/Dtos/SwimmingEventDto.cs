using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.Dtos
{
    public class SwimmingEventDto
    {
        public int Id { get; set; } // Primary Key

        public int SwimmingMeetId { get; set; } // Foregin Key

        [Required]
        public string Name { get; set; }


        [Required]
        public string Gender { get; set; }

        [Required]
        public string AgeRange { get; set; }

        [Required]
        public string Distance { get; set; }

        [Required]
        public string Stroke { get; set; }

        public string Round { get; set; }

        public virtual ICollection<LaneDto> Lanes { get; set; }

    }
}