using CollegeRoadApplication.Models;
using CollegeRoadApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            var Lane = new Lane();
            Lane.SwimmingEventId = id;

            var viewModel = new LaneFormViewModel
            {
                Lane = Lane,
                Swimmers = _context.Users.Where(m => m.isAllowedToSwim == true).ToList()
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

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {

            if (String.IsNullOrEmpty(id.ToString()))
            {
                // returns a bad request
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // It finds the User to be deleted.
            Lane lane = _context.Lanes.Find(id);

            if (lane == null)
            {
                // if doesn't found returns 404
                return HttpNotFound();
            }
            // Returns the Student data to show the details of what will be deleted.
            return View(lane);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ActionName("Delete")]         //Represents an attribute name of the get Action
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // Finds the User
                Lane lane = _context.Lanes.Find(id);

                // Try to remove it

                _context.Lanes.Remove(lane);

                // Save the changes
                _context.SaveChanges();
            }
            catch
            {
                //Log the error add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return RedirectToAction("Index", "SwimmingEvent");
        }
    }
}