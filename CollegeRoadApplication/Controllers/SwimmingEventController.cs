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
using CollegeRoadApplication.DAL;

namespace CollegeRoadApplication.Controllers
{
    public class SwimmingEventController : Controller
    {
        private ISwimmingEventRepository _swimmingEventRepository;

        public SwimmingEventController()
        {
            _swimmingEventRepository = new SwimmingEventRepository(new ApplicationDbContext());
        }

        public SwimmingEventController(ISwimmingEventRepository swimmingEventRepository)
        {
            _swimmingEventRepository = swimmingEventRepository;
        }

        protected override void Dispose(bool disposing)
        {
            _swimmingEventRepository.Dispose();
        }

        // GET: SwimmingEvent
        [AllowAnonymous]
        public ActionResult Index()
        {


            if (User.IsInRole("Swimmer"))
            {
                var currentUserId = User.Identity.GetUserId();

                var lanes = _swimmingEventRepository.GetMemberEventLanes(currentUserId);

                var swimmingEvents = new List<SwimmingEvent>();

                foreach (var lane in lanes)
                {
                    // should be single and defualt > instead of single 
                    swimmingEvents.Add(_swimmingEventRepository.GetSwimmingEventById(lane.SwimmingEventId));

                }

                return View("PersonalIndex", swimmingEvents);

            }
            else
            {
                var events = _swimmingEventRepository.GetAllSwimmingEvents();

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
                SwimmingMeet = _swimmingEventRepository.GetSwimmingMeetById(id),
                SwimmingEvents = _swimmingEventRepository.GetAllMeetEvents(id)
            };

            return View("AllMeetEvents", viewModel);
        }

        [AllowAnonymous]
        public PartialViewResult SearchEvent(string distance, string age, string gender, string stroke)
        {
            var events = _swimmingEventRepository.GetAllSwimmingEvents();
            var results = events.Where(e => e.Distance.ToLower().Contains(distance));
            results = results.Where(e => e.AgeRange.ToLower().Contains(age.ToLower()))
                             .Where(e => e.Gender.ToLower().StartsWith(gender.ToLower()))
                             .Where(e => e.Stroke.ToLower().StartsWith(stroke.ToLower()));

            return PartialView("_EventFilterGrid", results);

        }

        public ActionResult New()
        {

            var viewModel = new SwimmingEventFormViewModel
            {
                SwimmingEvent = new SwimmingEvent(),
                SwimmingMeets = _swimmingEventRepository.GetAllSwimmingMeets()

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
                    SwimmingMeets = _swimmingEventRepository.GetAllSwimmingMeets()
                };

                return View("SwimmingEventForm", viewModel);
            }

            if (swimmingEvent.Id == 0)
            {
                _swimmingEventRepository.Add(swimmingEvent);
            }
            else
            {
                var swimmingEventInDb = _swimmingEventRepository.GetSwimmingEventInDb(swimmingEvent.Id);

                swimmingEventInDb.Name = swimmingEvent.Name;
                swimmingEventInDb.SwimmingMeetId = swimmingEvent.SwimmingMeetId;
                swimmingEventInDb.Gender = swimmingEvent.Gender;
                swimmingEventInDb.AgeRange = swimmingEvent.AgeRange;
                swimmingEventInDb.Distance = swimmingEvent.Distance;
                swimmingEventInDb.Stroke = swimmingEvent.Stroke;
                swimmingEventInDb.Round = swimmingEvent.Round;


            }

            _swimmingEventRepository.Save();

            return RedirectToAction("Index", "SwimmingEvent");
        }

        /**
         * Param {id} - Swimming Event Id
         */
        [AllowAnonymous]
        public ActionResult Edit(int id)
        {
            var race = _swimmingEventRepository.GetSwimmingEventIncludeMeetById(id);
            race.Lanes = _swimmingEventRepository.GetAllLanesIncludeUser(id);

            if (race == null)
            {
                return HttpNotFound();
            }

            var viewModel = new SwimmingEventFormViewModel
            {
                SwimmingEvent = race,
                SwimmingMeets = _swimmingEventRepository.GetAllSwimmingMeets()
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
            SwimmingEvent swimmingEvent = _swimmingEventRepository.Find(id);

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
                SwimmingEvent swimmingEvent = _swimmingEventRepository.Find(id);

                // Try to remove it

                _swimmingEventRepository.Remove(swimmingEvent);

                // Save the changes
                _swimmingEventRepository.Save();
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
