using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.Models
{
    public class FamilyGroup
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Family Group")]
        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Members { get; set; }
    }
}