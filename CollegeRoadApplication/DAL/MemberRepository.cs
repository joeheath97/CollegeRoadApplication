using CollegeRoadApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.DAL
{
    public class MemberRepository : IMemberRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetAllMembers()
        {
            return _context.Users.ToList();
        }
        public IEnumerable<ApplicationUser> GetAllEligibleSwimmers()
        {
            return _context.Users.Where(m => m.isAllowedToSwim == true).ToList();
        }
        public IEnumerable<ApplicationUser> GetAllArchivedMembers()
        {
            return _context.Users.Where(m => m.IsArchived == true).ToList();
        }

        public IEnumerable<FamilyGroup> GetAllFamilyGroups()
        {
            return _context.FamilyGroups.ToList();
        }

        public ApplicationUser GetMemberById(string id)
        {
            return _context.Users.SingleOrDefault(m => m.Id == id);
        }

        public ApplicationUser GetMemberInDB(string id)
        {
            return _context.Users.Single(m => m.Id == id);
        }

        public ApplicationUser FindById(string id)
        {
            return _context.Users.Find(id);
        }

        public void Add(ApplicationUser user)
        {
           _context.Users.Add(user);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(ApplicationUser user)
        {
            _context.Users.Remove(user);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}