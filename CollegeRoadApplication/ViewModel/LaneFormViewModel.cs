using CollegeRoadApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.ViewModel
{
    public class LaneFormViewModel
    {
        public Lane Lane { get; set; }

        public IEnumerable<ApplicationUser> Swimmers { get; set; }

        public string Title
        {
            get
            {
                if (Lane != null && Lane.Id != 0)
                {
                    return "Edit";
                }

                return "New";
            }
        }
    }
}