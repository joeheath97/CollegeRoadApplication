using CollegeRoadApplication.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public PartialViewResult SearchMembers(string searchText)
        {

            var members = _context.Users.ToList();
            var searchResults = members.Where(m => m.Age.ToString().Contains(searchText) || m.Name.ToLower().Contains(searchText.ToLower()));

            return PartialView("_MemberFilterGrid", searchResults);
        }


        /**
         * Param {id} = the name of the user
         */
        public ActionResult Edit(string id)
        {

            var member = _context.Users.SingleOrDefault(m => m.Name == id);

            if (member == null)
            {
                return HttpNotFound();
            }

            return View("MemberForm", member);
        }

        [HttpPost]
        public ActionResult Save(ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {

                return View("MemberForm", user);
            }

            if(user.Id == "")
            {
                _context.Users.Add(user);
            }
            else
            {
                var userInDb = _context.Users.Single(m => m.Name == user.Name);

                userInDb.Name = user.Name;
                userInDb.UserName = user.UserName;
                userInDb.Email = user.Email;
                userInDb.Gender = user.Gender;
                userInDb.ContactNumber = user.ContactNumber;
                userInDb.Age = user.Age;
                userInDb.isAllowedToSwim = user.isAllowedToSwim;
                userInDb.IsArchived = user.IsArchived;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Member");
        }

        public ActionResult Archive()
        {
            var members = _context.Users.Where(m => m.IsArchived == true);

            return View(members);
        }
    }
}