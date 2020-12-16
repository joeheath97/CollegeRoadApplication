using CollegeRoadApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.ViewModel
{
    public class SwimmingMeetFormViewModel
    {
        public SwimmingMeet SwimmingMeet { get; set; }
        public IEnumerable<PoolSizeType> PoolSizeTypes { get; set; }

        public string Title
        {
            get
            {
                if (SwimmingMeet != null && SwimmingMeet.Id != 0)
                {
                    return "Edit";
                }

                return "New";
            }
        }
    }
}