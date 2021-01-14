using CollegeRoadApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.DAL
{
    public class FamilyGroupRepository : IFamilyGroupRepository
    {
        private ApplicationDbContext _context;

        public FamilyGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<FamilyGroup> GetAllFamilyGroups()
        {
            return _context.FamilyGroups.ToList();
        }

        public IEnumerable<FamilyGroup> GetUserFamilyGroup(int? id)
        {
            return _context.FamilyGroups.Where(m => m.Id == id);
        }

        public ICollection<ApplicationUser> GetMembersInFamilyGroup(int id)
        {
            return _context.Users.Where(m => m.FamilyGroupId == id).OrderBy(x => x.UserName).ToList();
        }

        public FamilyGroup GetFamilyGroupInDb(int id)
        {
            return _context.FamilyGroups.Single(f => f.Id == id);
        }

        public FamilyGroup GetFamilyGroupById(int id)
        {
            return _context.FamilyGroups.SingleOrDefault(c => c.Id == id);
        }

        public ApplicationUser GetUserById(string id)
        {
            return _context.Users.Single(m => m.Id == id);
        }

        public FamilyGroup FindById(int id)
        {
            return _context.FamilyGroups.Find(id);
        }

        public void Add(FamilyGroup familyGroup)
        {
            _context.FamilyGroups.Add(familyGroup);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(FamilyGroup familyGroup)
        {
            _context.FamilyGroups.Remove(familyGroup);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}