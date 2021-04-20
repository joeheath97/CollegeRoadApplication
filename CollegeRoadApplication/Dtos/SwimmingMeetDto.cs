using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.Dtos
{
    public class SwimmingMeetDto
    {
        public int Id { get; set; } // Primary Key

        public byte PoolSizeTypeId { get; set; } // Foregin Key

        [Required]
        public string Name { get; set; }

        public string Vanue { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<SwimmingEventDto> SwimmingEvents { get; set; } // Collection Navigation property > Swimming Meet can have multiple events

    }
}