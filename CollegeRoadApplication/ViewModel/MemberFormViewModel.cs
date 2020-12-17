using CollegeRoadApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.ViewModel
{
    public class MemberFormViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }

        public IEnumerable<FamilyGroup> FamilyGroups { get; set; }

        public string Title
        {
            get
            {
                if (ApplicationUser != null && ApplicationUser.Id != "")
                {
                    return "Edit";
                }

                return "New";
            }
        }
    }
}