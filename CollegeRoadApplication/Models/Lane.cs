using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.Models
{
    public class Lane
    {
        public int Id { get; set; } // Primary Key 

        [Display(Name = "Swimmer")]
        public int? ApplicationUserId { get; set; } // Foregin Key 

        public int SwimmingEventId { get; set; } // Foregin Key

        public string Result { get; set; }

        public ApplicationUser ApplicationUser { get; set; } // Single Navigation Property

        public SwimmingEvent SwimmingEvent { get; set; } // Single Navigation Property 
    }
}