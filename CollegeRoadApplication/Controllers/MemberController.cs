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
    public class MemberController : Controller
    {
        private ApplicationDbContext _context;

        public MemberController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Member
        public ActionResult Index()
        {
            var users = _context.Users.ToList();

            return View(users);
        }

        public ActionResult AllSwimmers()
        {
            var swimmers = _context.Users.ToList();

            return View(swimmers);
        }

        public ActionResult Archive()
        {
            var members = _context.Users.Where(m => m.IsArchived == true).ToList();

            return View(members);
        }

        public PartialViewResult SearchMembers(string searchText)
        {

            var members = _context.Users.ToList();
            var searchResults = members.Where(m => m.Age.ToString().Contains(searchText) || m.Name.ToLower().Contains(searchText.ToLower()));

            return PartialView("_MemberFilterGrid", searchResults);
        }

        public PartialViewResult SearchSwimmer(string searchText)
        {

            var members = _context.Users.ToList();
            var searchResults = members.Where(m => m.Age.ToString().Contains(searchText) || m.Name.ToLower().Contains(searchText.ToLower()));

            return PartialView("_SwimmerFilterGrid", searchResults);
        }


        /**
         * Param {id} = the Id of the Application User
         */
        public ActionResult Edit(string id)
        {

            var member = _context.Users.SingleOrDefault(m => m.Id == id);

            if (member == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MemberFormViewModel
            {
                ApplicationUser = member,
                FamilyGroups = _context.FamilyGroups.ToList()
            };

            return View("MemberForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(MemberFormViewModel user)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MemberFormViewModel
                {
                    ApplicationUser = user.ApplicationUser,
                    FamilyGroups = _context.FamilyGroups.ToList()
                };

                return View("MemberForm", viewModel);
            }

            if(user.ApplicationUser.Id == "")
            {
                _context.Users.Add(user.ApplicationUser);
            }
            else
            {
                var userInDb = _context.Users.Single(m => m.Id == user.ApplicationUser.Id);

                userInDb.Name = user.ApplicationUser.Name;
                userInDb.UserName = user.ApplicationUser.UserName;
                userInDb.Email = user.ApplicationUser.Email;
                userInDb.Gender = user.ApplicationUser.Gender;
                userInDb.ContactNumber = user.ApplicationUser.ContactNumber;
                userInDb.Age = user.ApplicationUser.Age;
                userInDb.isAllowedToSwim = user.ApplicationUser.isAllowedToSwim;
                userInDb.IsArchived = user.ApplicationUser.IsArchived;
                userInDb.FamilyGroupId = user.ApplicationUser.FamilyGroupId;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Member");
        }

        // GET: Student/Delete/5
        public ActionResult Delete(string id)
        {

            if (id == null)
            {
                // returns a bad request
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // It finds the User to be deleted.
            ApplicationUser user = _context.Users.Find(id);

            if (user == null)
            {
                // if doesn't found returns 404
                return HttpNotFound();
            }
            // Returns the Student data to show the details of what will be deleted.
            return View(user);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ActionName("Delete")]         //Represents an attribute name of the get Action
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                // Finds the User
                ApplicationUser user = _context.Users.Find(id);

                // Try to remove it
                _context.Users.Remove(user);

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