using CollegeRoadApplication.DAL;
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

        private ILaneRepository _laneRepository;

        public LaneController()
        {
            _laneRepository = new LaneRepository(new ApplicationDbContext());
        }

        public LaneController(ILaneRepository laneRepository)
        {
            _laneRepository = laneRepository;
        }

        protected override void Dispose(bool disposing)
        {
            _laneRepository.Dispose();
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
                Swimmers = _laneRepository.GetAllEligiableSwimmers()
                
            };

            return View("LaneForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Lane lane)
        {

            if (lane.Id == 0)
            {
                _laneRepository.Add(lane);
            }
            else
            {
                var laneInDb = _laneRepository.GetLaneInDb(lane.Id);

                laneInDb.ApplicationUserId = lane.ApplicationUserId;
                laneInDb.Result = lane.Result;
                laneInDb.SwimmingEventId = lane.SwimmingEventId;
            }

            _laneRepository.Save();

            return RedirectToAction("Edit", "SwimmingEvent", new { id = lane.SwimmingEventId });
        }


        /**
         * Param {id} - Lane Id
         */
        public ActionResult Edit(int id)
        {
            var lane = _laneRepository.GetLaneById(id);

            if (lane == null)
            {
                return HttpNotFound();
            }

            var viewModel = new LaneFormViewModel
            {
                Lane = lane,
                Swimmers = _laneRepository.GetAllEligiableSwimmers()
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
            Lane lane = _laneRepository.FindById(id);

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
                Lane lane = _laneRepository.FindById(id);

                _laneRepository.Remove(lane);

                _laneRepository.Save();

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