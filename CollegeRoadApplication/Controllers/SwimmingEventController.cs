using CollegeRoadApplication.Models;
using CollegeRoadApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Net;

namespace CollegeRoadApplication.Controllers
{
    public class SwimmingEventController : Controller
    {
        private ApplicationDbContext _context;

        public SwimmingEventController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: SwimmingEvent
        [AllowAnonymous]
        public ActionResult Index()
        {


            if (User.IsInRole("Swimmer"))
            {
                var currentUserId = User.Identity.GetUserId();

                var lanes = _context.Lanes.Where(m => m.ApplicationUserId == currentUserId).ToList();

                var swimmingEvents = new List<SwimmingEvent>();

                foreach (var lane in lanes)
                {
                    swimmingEvents.Add(_context.SwimmingEvents.Single(l => l.Id == lane.SwimmingEventId));
                    
                }

                return View("PersonalIndex", swimmingEvents);

            }
            else
            {
                var events = _context.SwimmingEvents.ToList();

                return View(events);
            }

            
        }

        /**
         * Param {id} - Swimming Meet Id 
         */
        [AllowAnonymous]
        public ActionResult AllMeetEvents(int id)
        {
            var viewModel = new AllMeetEventsViewModel
            {
                SwimmingMeet = _context.SwimmingMeets.Single(e => e.Id == id),
                SwimmingEvents = _context.SwimmingEvents.Where(e => e.SwimmingMeetId == id)
            };

            return View("AllMeetEvents", viewModel);
        }

        [AllowAnonymous]
        public PartialViewResult SearchEvent(string distance, string age, string gender)
        {
            var events = _context.SwimmingEvents.ToList();
            var results = events.Where(e => e.Distance.ToLower().Contains(distance));
            results = results.Where(e => e.AgeRange.ToLower().Contains(age.ToLower()))
                             .Where(e => e.Gender.ToLower().StartsWith(gender.ToLower()));

            return PartialView("_EventFilterGrid", results);

        }

        public ActionResult New()
        {

            var viewModel = new SwimmingEventFormViewModel
            {
                SwimmingEvent = new SwimmingEvent(),
                SwimmingMeets = _context.SwimmingMeets.ToList()

            };

            return View("SwimmingEventForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(SwimmingEvent swimmingEvent)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new SwimmingEventFormViewModel
                {
                    SwimmingEvent = swimmingEvent,
                    SwimmingMeets = _context.SwimmingMeets.ToList()
                };

                return View("SwimmingEventForm", viewModel);
            }

            if (swimmingEvent.Id == 0)
            {
                _context.SwimmingEvents.Add(swimmingEvent);
            }
            else
            {
                var swimmingEventInDb = _context.SwimmingEvents.Single(e => e.Id == swimmingEvent.Id);

                swimmingEventInDb.Name = swimmingEvent.Name;
                swimmingEventInDb.SwimmingMeetId = swimmingEvent.SwimmingMeetId;
                swimmingEventInDb.Gender = swimmingEvent.Gender;
                swimmingEventInDb.AgeRange = swimmingEvent.AgeRange;
                swimmingEventInDb.Distance = swimmingEvent.Distance;
                swimmingEventInDb.Stroke = swimmingEvent.Stroke;
                swimmingEventInDb.Round = swimmingEvent.Round;


            }

            _context.SaveChanges();

            return RedirectToAction("Index", "SwimmingEvent");
        }

        /**
         * Param {id} - Swimming Event Id
         */
        [AllowAnonymous]
        public ActionResult Edit(int id)
        {
            var race = _context.SwimmingEvents.Include(c => c.SwimmingMeet).SingleOrDefault(r => r.Id == id);
            race.Lanes = _context.Lanes.Include(u => u.ApplicationUser).Where(c => c.SwimmingEventId == id).ToList();

            if (race == null)
            {
                return HttpNotFound();
            }

            var viewModel = new SwimmingEventFormViewModel
            {
                SwimmingEvent = race,
                SwimmingMeets = _context.SwimmingMeets.ToList()
            };

            if (User.IsInRole("Admin") || User.IsInRole("SCO"))
            {
                return View("SwimmingEventForm", viewModel);
            }
            else
            {

                return View("ReadOnlySwimmingEventForm", viewModel);

            }

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
            SwimmingEvent swimmingEvent = _context.SwimmingEvents.Find(id);

            if (swimmingEvent == null)
            {
                // if doesn't found returns 404
                return HttpNotFound();
            }
            // Returns the Student data to show the details of what will be deleted.
            return View(swimmingEvent);
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
                SwimmingEvent swimmingEvent = _context.SwimmingEvents.Find(id);

                // Try to remove it
                _context.SwimmingEvents.Remove(swimmingEvent);

                // Save the changes
                _context.SaveChanges();
            }
            catch
            {
                //Log the error add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return RedirectToAction("Index");
        }

    }
}
