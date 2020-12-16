using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.Models
{
    public class SwimmingEvent
    {
        public int Id { get; set; } // Primary Key

        [Display(Name = "Swimming Meet")]
        public int SwimmingMeetId { get; set; } // Foregin Key

        [Required]
        public string Name { get; set; }


        [Required]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Age Range")]
        public string AgeRange { get; set; }

        [Required]
        public string Distance { get; set; }

        [Required]
        public string Stroke { get; set; }

        public string Round { get; set; }

        public SwimmingMeet SwimmingMeet { get; set; } // Single Navigation Property

        public virtual ICollection<Lane> Lanes { get; set; } // Collection Navigation Property > Event can have multiple lanes 
    }
}