using CollegeRoadApplication.DAL;
using CollegeRoadApplication.Models;
using CollegeRoadApplication.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CollegeRoadApplication.Controllers
{
    public class SwimmingMeetController : Controller
    {

        private ISwimmingMeetRepository _swimmingMeetRepository;

        public SwimmingMeetController()
        {
            _swimmingMeetRepository = new SwimmingMeetRepository(new ApplicationDbContext());
        }

        protected override void Dispose(bool disposing)
        {
            _swimmingMeetRepository.Dispose();
        }

        // GET: SwimmingMeet
        [AllowAnonymous]
        public ActionResult Index()
        {
            var meets = _swimmingMeetRepository.GetAllSwimmingMeets();

            return View(meets);
        }

        [AllowAnonymous]
        public PartialViewResult SearchMeet(string venue, string Date)
        {
            var meets = _swimmingMeetRepository.GetAllSwimmingMeets();

            var results = meets.Where(v => v.Vanue.ToLower().Contains(venue.ToLower()));
            results = results.Where(v => v.Date.Year.ToString().Contains(Date));

            return PartialView("_MeetFilterGrid", results);
        }

        public ActionResult New()
        {
            var viewModel = new SwimmingMeetFormViewModel
            {
                SwimmingMeet = new SwimmingMeet(),
                PoolSizeTypes = _swimmingMeetRepository.GetAllPoolSizeTypes()
            };

            return View("SwimmingMeetForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(SwimmingMeet swimmingMeet)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new SwimmingMeetFormViewModel
                {
                    SwimmingMeet = swimmingMeet,
                    PoolSizeTypes = _swimmingMeetRepository.GetAllPoolSizeTypes()
                };

                return View("SwimmingMeetForm", viewModel);
            }

            if (swimmingMeet.Id == 0)
            {
                _swimmingMeetRepository.Add(swimmingMeet);
            }
            else
            {
                var swimmingMeetInDb = _swimmingMeetRepository.GetSwimmingMeetById(swimmingMeet.Id);


                swimmingMeetInDb.Name = swimmingMeet.Name;
                swimmingMeetInDb.Vanue = swimmingMeet.Vanue;
                swimmingMeetInDb.Date = swimmingMeet.Date;
                swimmingMeetInDb.PoolSizeTypeId = swimmingMeet.PoolSizeTypeId;

            }

            _swimmingMeetRepository.Save();

            return RedirectToAction("Index", "SwimmingMeet");
        }

        /**
         * Param {id} > Swimming Meet Id
         */
        public ActionResult Edit(int id)
        {
            var meet = _swimmingMeetRepository.GetSwimmingMeetInDB(id);

            if (meet == null)
            {
                return HttpNotFound();
            }

            var viewModel = new SwimmingMeetFormViewModel
            {
                SwimmingMeet = meet,
                PoolSizeTypes = _swimmingMeetRepository.GetAllPoolSizeTypes()
            };

            return View("SwimmingMeetForm", viewModel);
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
            SwimmingMeet meet = _swimmingMeetRepository.FindById(id);

            if (meet == null)
            {
                // if doesn't found returns 404
                return HttpNotFound();
            }
            // Returns the Student data to show the details of what will be deleted.
            return View(meet);
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
                SwimmingMeet meet = _swimmingMeetRepository.FindById(id);

                // Try to remove it
                _swimmingMeetRepository.Remove(meet);

                // Save the changes
                _swimmingMeetRepository.Save();
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