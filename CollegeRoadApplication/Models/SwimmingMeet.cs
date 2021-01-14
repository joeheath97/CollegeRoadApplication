using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.Models
{
    public class SwimmingMeet
    {
        public int Id { get; set; } // Primary Key

        [Display(Name = "Pool Size")]
        public byte PoolSizeTypeId { get; set; } // Foregin Key

        [Required]
        [Display(Name = "Swimming Meet")]
        public string Name { get; set; }

        public string Vanue { get; set; }

        [Display(Name = "Date of Meet (dd/MM/yyyy)")]
        public DateTime Date { get; set; }

        public virtual ICollection<SwimmingEvent> SwimmingEvents { get; set; } // Collection Navigation property > Swimming Meet can have multiple events

        public PoolSizeType PoolSizeType { get; set; } // Single Navigation Property
    }
}