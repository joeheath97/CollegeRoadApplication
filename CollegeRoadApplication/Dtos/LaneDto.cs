using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.Dtos
{
    public class LaneDto
    {
        public int Id { get; set; } // Primary Key 

        public string ApplicationUserId { get; set; } // Foregin Key 

        public int SwimmingEventId { get; set; } // Foregin Key

        public string Result { get; set; }

    }
}