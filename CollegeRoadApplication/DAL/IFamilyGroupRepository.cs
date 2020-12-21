using CollegeRoadApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeRoadApplication.DAL
{
    public interface IFamilyGroupRepository : IDisposable
    {
        IEnumerable<FamilyGroup> GetAllFamilyGroups();

        IEnumerable<FamilyGroup> GetUserFamilyGroup(int? id);

        ICollection<ApplicationUser> GetMembersInFamilyGroup(int id);

        ApplicationUser GetUserById(string id);
        FamilyGroup GetFamilyGroupById(int id);
        FamilyGroup GetFamilyGroupInDb(int id);

        FamilyGroup FindById(int id);

        void Add(FamilyGroup familyGroup);

        void Save();

        void Remove(FamilyGroup familyGroup);
    }
}
