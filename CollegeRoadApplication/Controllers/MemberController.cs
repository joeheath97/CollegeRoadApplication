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
    public class MemberController : Controller
    {
        private IMemberRepository _memberRepository;

        public MemberController()
        {
            _memberRepository = new MemberRepository(new ApplicationDbContext());
        }

        public MemberController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        protected override void Dispose(bool disposing)
        {
            _memberRepository.Dispose();
        }

        // GET: Member
        public ActionResult Index()
        {
            var users = _memberRepository.GetAllMembers();

            return View(users);
        }

        public ActionResult AllSwimmers()
        {
            var swimmers = _memberRepository.GetAllMembers();

            return View(swimmers);
        }

        public ActionResult Archive()
        {
            var members = _memberRepository.GetAllArchivedMembers();

            return View(members);
        }

        public PartialViewResult SearchMembers(string searchText)
        {

            var members = _memberRepository.GetAllMembers();

            var searchResults = members.Where(m => m.Age.ToString().Contains(searchText) || m.Name.ToLower().Contains(searchText.ToLower()));

            return PartialView("_MemberFilterGrid", searchResults);
        }

        public PartialViewResult SearchSwimmer(string searchText)
        {

            var members = _memberRepository.GetAllEligibleSwimmers();

            var searchResults = members.Where(m => m.Age.ToString().Contains(searchText) || m.Name.ToLower().Contains(searchText.ToLower()));

            return PartialView("_SwimmerFilterGrid", searchResults);
        }


        /**
         * Param {id} = the Id of the Application User
         */
        public ActionResult Edit(string id)
        {

            var member = _memberRepository.GetMemberById(id);

            if (member == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MemberFormViewModel
            {
                ApplicationUser = member,
                FamilyGroups = _memberRepository.GetAllFamilyGroups()
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
                    FamilyGroups = _memberRepository.GetAllFamilyGroups()
                };

                return View("MemberForm", viewModel);
            }

            if(user.ApplicationUser.Id == "")
            {
                _memberRepository.Add(user.ApplicationUser);
            }
            else
            {
                var userInDb = _memberRepository.GetMemberInDB(user.ApplicationUser.Id);

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

            _memberRepository.Save();

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
            ApplicationUser user = _memberRepository.FindById(id);

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
                ApplicationUser user = _memberRepository.FindById(id);

                // Try to remove it
                _memberRepository.Remove(user);

                // Save the changes
                _memberRepository.Save();
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