using CollegeRoadApplication.DAL;
using CollegeRoadApplication.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CollegeRoadApplication.Controllers
{
    public class FamilyGroupController : Controller
    {
        private IFamilyGroupRepository _familyGroupRepository;

        public FamilyGroupController()
        {
            _familyGroupRepository = new FamilyGroupRepository(new ApplicationDbContext());
        }

        public FamilyGroupController(IFamilyGroupRepository familyGroupRepository)
        {
            _familyGroupRepository = familyGroupRepository;
        }

        protected override void Dispose(bool disposing)
        {
            // this release the connection 
            _familyGroupRepository.Dispose();
        }

        // GET: FamilyGroup
        public ActionResult Index()
        {
            var familyGroup = _familyGroupRepository.GetAllFamilyGroups();

            if (User.IsInRole("Parent"))
            {
                var currentUserId = User.Identity.GetUserId();

                var user = _familyGroupRepository.GetUserById(currentUserId);

                var linkedFamilyGroup = _familyGroupRepository.GetUserFamilyGroup(user.FamilyGroupId);

                return View(linkedFamilyGroup);
            }


            return View(familyGroup);
        }


        public ActionResult New()
        {
            var familyGroup = new FamilyGroup();

            return View(familyGroup);
        }

        [HttpPost]
        public ActionResult Save(FamilyGroup familyGroup)
        {
            if (!ModelState.IsValid)
            {
                return View("FamilyGroupForm", familyGroup);
            }

            if (familyGroup.Id == 0)
            {
                _familyGroupRepository.Add(familyGroup);
            }
            else
            {
                var familyGroupInDb = _familyGroupRepository.GetFamilyGroupInDb(familyGroup.Id);

                familyGroupInDb.Name = familyGroup.Name;

            }

            _familyGroupRepository.Save();

            return RedirectToAction("Index", "FamilyGroup");
        }

        /**
         * Param {id} - Family Group Id
         */
        public ActionResult Edit(int id)
        {
            var familygroup = _familyGroupRepository.GetFamilyGroupById(id);
          
            familygroup.Members = _familyGroupRepository.GetMembersInFamilyGroup(id);
            
            //familygroup.Parents = _context.Parents.Where(m => m.FamilyGroupId == id).ToList();

            if (familygroup == null)
            {
                return HttpNotFound();
            }

            return View("FamilyGroupForm", familygroup);
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
            FamilyGroup familyGroup = _familyGroupRepository.FindById(id);

            if (familyGroup == null)
            {
                // if doesn't found returns 404
                return HttpNotFound();
            }
            // Returns the Student data to show the details of what will be deleted.
            return View(familyGroup);
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
                FamilyGroup familyGroup = _familyGroupRepository.FindById(id);


                // Try to remove it
                _familyGroupRepository.Remove(familyGroup);

                // Save the changes
                _familyGroupRepository.Save();
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