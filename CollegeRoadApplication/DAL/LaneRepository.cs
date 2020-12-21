using CollegeRoadApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeRoadApplication.DAL
{
    public class LaneRepository : ILaneRepository
    {
        private ApplicationDbContext _context;

        public LaneRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetAllEligiableSwimmers()
        {
            return _context.Users.Where(m => m.isAllowedToSwim == true).ToList();
        }

        public Lane GetLaneById(int id)
        {
            return _context.Lanes.SingleOrDefault(c => c.Id == id);
        }

        public Lane GetLaneInDb(int id)
        {
            return _context.Lanes.Single(l => l.Id == id);
        }
        public Lane FindById(int id)
        {
            return _context.Lanes.Find(id);
        }

        public void Add(Lane lane)
        {
            _context.Lanes.Add(lane);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(Lane lane)
        {
            _context.Lanes.Remove(lane);
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}