using CollegeRoadApplication.Models;
using CollegeRoadApplication.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CollegeRoadApplication.Controllers
{
    public class SwimmingMeetController : Controller
    {
        private ApplicationDbContext _context;

        public SwimmingMeetController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: SwimmingMeet
        [AllowAnonymous]
        public ActionResult Index()
        {
            var meets = _context.SwimmingMeets.ToList();

            return View(meets);
        }

        [AllowAnonymous]
        public PartialViewResult SearchMeet(string venue, string Date)
        {
            var meets = _context.SwimmingMeets.ToList();

            var results = meets.Where(v => v.Vanue.ToLower().Contains(venue.ToLower()));
            results = results.Where(v => v.Date.Year.ToString().Contains(Date));

            return PartialView("_MeetFilterGrid", results);
        }

        public ActionResult New()
        {
            var viewModel = new SwimmingMeetFormViewModel
            {
                SwimmingMeet = new SwimmingMeet(),
                PoolSizeTypes = _context.PoolSizeTypes.ToList()
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
                    PoolSizeTypes = _context.PoolSizeTypes.ToList()
                };

                return View("SwimmingMeetForm", viewModel);
            }

            if (swimmingMeet.Id == 0)
            {
                _context.SwimmingMeets.Add(swimmingMeet);
            }
            else
            {
                var swimmingMeetInDb = _context.SwimmingMeets.Single(m => m.Id == swimmingMeet.Id);

                swimmingMeetInDb.Name = swimmingMeet.Name;
                swimmingMeetInDb.Vanue = swimmingMeet.Vanue;
                swimmingMeetInDb.Date = swimmingMeet.Date;
                swimmingMeetInDb.PoolSizeTypeId = swimmingMeet.PoolSizeTypeId;

            }

            _context.SaveChanges();

            return RedirectToAction("Index", "SwimmingMeet");
        }

        /**
         * Param {id} > Swimming Meet Id
         */
        public ActionResult Edit(int id)
        {
            var meet = _context.SwimmingMeets.SingleOrDefault(m => m.Id == id);

            if (meet == null)
            {
                return HttpNotFound();
            }

            var viewModel = new SwimmingMeetFormViewModel
            {
                SwimmingMeet = meet,
                PoolSizeTypes = _context.PoolSizeTypes.ToList()
            };

            return View("SwimmingMeetForm", viewModel);
        }
    }
}