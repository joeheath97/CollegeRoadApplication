using CollegeRoadApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace CollegeRoadApplication.DAL
{
    public class SwimmingEventRepository : ISwimmingEventRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public SwimmingEventRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public ICollection<Lane> GetAllLanesIncludeUser(int id)
        {
            return _context.Lanes.Include(u => u.ApplicationUser).Where(c => c.SwimmingEventId == id).ToList();
        }

        public IEnumerable<SwimmingEvent> GetAllMeetEvents(int id)
        {
            return _context.SwimmingEvents.Where(e => e.SwimmingMeetId == id);
        }

        public IEnumerable<SwimmingEvent> GetAllSwimmingEvents()
        {
            return _context.SwimmingEvents.Include(x => x.SwimmingMeet).ToList();
        }

        public IEnumerable<Lane> GetMemberEventLanes(string currentUserId)
        {
            return _context.Lanes.Where(m => m.ApplicationUserId == currentUserId).ToList();
        }

        public IEnumerable<SwimmingMeet> GetAllSwimmingMeets()
        {
            return _context.SwimmingMeets.ToList();
        }

        public SwimmingEvent GetSwimmingEventById(int id)
        {
            return _context.SwimmingEvents.Single(l => l.Id == id);
        }

        public SwimmingEvent GetSwimmingEventIncludeMeetById(int id)
        {
            return _context.SwimmingEvents.Include(c => c.SwimmingMeet).SingleOrDefault(r => r.Id == id);
        }

        public SwimmingEvent GetSwimmingEventInDb(int id)
        {
            return _context.SwimmingEvents.Single(e => e.Id == id);
        }

        public SwimmingMeet GetSwimmingMeetById(int id)
        {
            return _context.SwimmingMeets.Single(e => e.Id == id);
        }

        public SwimmingEvent Find(int id)
        {
            return _context.SwimmingEvents.Find(id);
        }

        public void Remove(SwimmingEvent swimmingEvent)
        {
            _context.SwimmingEvents.Remove(swimmingEvent);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Add(SwimmingEvent swimmingEvent)
        {
            _context.SwimmingEvents.Add(swimmingEvent);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}