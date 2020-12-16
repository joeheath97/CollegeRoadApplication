using CollegeRoadApplication.Models;
using CollegeRoadApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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

        public ActionResult Index()
        {
            var events = _context.SwimmingEvents.ToList();

            return View(events);
        }

        /**
         * Param {id} - Swimming Meet Id 
         */ 
        public ActionResult AllMeetEvents(int id)
        {
            var viewModel = new AllMeetEventsViewModel
            {
                SwimmingMeet = _context.SwimmingMeets.Single(e => e.Id == id),
                SwimmingEvents = _context.SwimmingEvents.Where(e => e.SwimmingMeetId == id)
            };

            return View("AllMeetEvents", viewModel);
        }

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
        public ActionResult Edit(int id)
        {
            var race = _context.SwimmingEvents.Include(c => c.SwimmingMeet).SingleOrDefault(r => r.Id == id);
            race.Lanes = _context.Lanes.Where(c => c.SwimmingEventId == id).ToList();

            if (race == null)
            {
                return HttpNotFound();
            }

            var viewModel = new SwimmingEventFormViewModel
            {
                SwimmingEvent = race,
                SwimmingMeets = _context.SwimmingMeets.ToList()
            };


            return View("SwimmingEventForm", viewModel);

        }


    }
}
