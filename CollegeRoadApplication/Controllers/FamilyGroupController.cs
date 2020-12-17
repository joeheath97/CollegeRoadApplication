using CollegeRoadApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CollegeRoadApplication.Controllers
{
    public class FamilyGroupController : Controller
    {
        private ApplicationDbContext _context;

        public FamilyGroupController()
        {
            // this creates a connection to the database
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            // this release the connection 
            _context.Dispose();
        }

        // GET: FamilyGroup
        public ActionResult Index()
        {
            var familyGroup = _context.FamilyGroups.ToList();

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
                _context.FamilyGroups.Add(familyGroup);
            }
            else
            {
                var failyGroupInDb = _context.FamilyGroups.Single(f => f.Id == familyGroup.Id);
                familyGroup.Name = familyGroup.Name;

            }
            _context.SaveChanges();

            return RedirectToAction("Index", "FamilyGroup");
        }

        /**
         * Param {id} - Family Group Id
         */
        public ActionResult Edit(int id)
        {
            var familygroup = _context.FamilyGroups.SingleOrDefault(c => c.Id == id);
            familygroup.Members = _context.Users.Where(m => m.FamilyGroupId == id).ToList();
            //familygroup.Parents = _context.Parents.Where(m => m.FamilyGroupId == id).ToList();

            if (familygroup == null)
            {
                return HttpNotFound();
            }

            return View("FamilyGroupForm", familygroup);
        }
    }
}