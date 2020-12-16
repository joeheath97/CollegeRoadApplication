using CollegeRoadApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.ViewModel
{
    public class SwimmingEventFormViewModel
    {
        public SwimmingEvent SwimmingEvent { get; set; }

        public IEnumerable<SwimmingMeet> SwimmingMeets { get; set; }

        public string Title
        {
            get
            {
                if (SwimmingEvent != null && SwimmingEvent.Id != 0)
                {
                    return "Edit";
                }

                return "New";
            }
        }
    }
}