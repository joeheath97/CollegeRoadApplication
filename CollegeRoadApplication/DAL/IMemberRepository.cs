using CollegeRoadApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeRoadApplication.DAL
{
    public interface IMemberRepository : IDisposable
    {
        IEnumerable<ApplicationUser> GetAllMembers();
        IEnumerable<ApplicationUser> GetAllEligibleSwimmers();
        IEnumerable<ApplicationUser> GetAllArchivedMembers();

        IEnumerable<FamilyGroup> GetAllFamilyGroups();

        // Single or Default > If not found returns default : null
        ApplicationUser GetMemberById(string id);

        // Single > must be in the table
        ApplicationUser GetMemberInDB(string id);

        ApplicationUser FindById(string id);

        void Add(ApplicationUser user);
        void Save();

        void Remove(ApplicationUser user);

    }
}
