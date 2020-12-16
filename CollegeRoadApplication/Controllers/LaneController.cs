using CollegeRoadApplication.Models;
using CollegeRoadApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;

namespace CollegeRoadApplication.Controllers
{
    public class LaneController : Controller
    {
        private ApplicationDbContext _context;

        public LaneController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Lane
        /**
         * Param {id} - Swimming Event Id
         */
        public ActionResult New(int id)
        {
            var swimmerRoleId = _context.Roles.Where(r => r.Name == "Swimmer").ToString();
            //var swimmers = _context.Users.Where(u => u.Roles.Any(r => r.RoleId == swimmerRoleId));
            //var swimmers =  _context.Users.Where(x => x.Roles.Select(y => y.UserId).Contains(swimmerRoleId)).ToList();

            var Lane = new Lane();
            Lane.SwimmingEventId = id;

            var viewModel = new LaneFormViewModel
            {
                Lane = Lane,
                Swimmers = _context.Users.ToList()
            };

            return View("LaneForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Lane lane)
        {

            if (lane.Id == 0)
            {
                _context.Lanes.Add(lane);
            }
            else
            {
                var laneInDb = _context.Lanes.Single(l => l.Id == lane.Id);

                laneInDb.ApplicationUserId = lane.ApplicationUserId;
                laneInDb.Result = lane.Result;
                laneInDb.SwimmingEventId = lane.SwimmingEventId;
            }

            _context.SaveChanges();

            return RedirectToAction("Edit", "SwimmingEvent", new { id = lane.SwimmingEventId });
        }


        /**
         * Param {id} - Lane Id
         */
        public ActionResult Edit(int id)
        {
            var lane = _context.Lanes.SingleOrDefault(c => c.Id == id);

            if (lane == null)
            {
                return HttpNotFound();
            }

            var viewModel = new LaneFormViewModel
            {
                Lane = lane,
                Swimmers = _context.Users.ToList()
            };

            return View("LaneForm", viewModel);
        }
    }
}