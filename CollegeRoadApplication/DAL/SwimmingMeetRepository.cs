using CollegeRoadApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.DAL
{
    public class SwimmingMeetRepository : ISwimmingMeetRepository
    {
        private ApplicationDbContext _context;

        public SwimmingMeetRepository(ApplicationDbContext  context)
        {
            _context = context;
        }

        public IEnumerable<PoolSizeType> GetAllPoolSizeTypes()
        {
            return _context.PoolSizeTypes.ToList();
        }

        public IEnumerable<SwimmingMeet> GetAllSwimmingMeets()
        {
            return _context.SwimmingMeets.ToList();
        }

        public SwimmingMeet GetSwimmingMeetById(int id)
        {
            return _context.SwimmingMeets.Single(m => m.Id == id);
        }

        public SwimmingMeet GetSwimmingMeetInDB(int id)
        {
            return _context.SwimmingMeets.SingleOrDefault(m => m.Id == id);
        }

        public void Remove(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Add(SwimmingMeet swimmingMeet)
        {
            _context.SwimmingMeets.Add(swimmingMeet);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}