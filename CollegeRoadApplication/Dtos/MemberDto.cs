using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.Dtos
{
    public class MemberDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string ContactNumber { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public bool isAllowedToSwim { get; set; }

        public bool IsArchived { get; set; }

        public int? FamilyGroupId { get; set; } // Foregin Key

    }
}