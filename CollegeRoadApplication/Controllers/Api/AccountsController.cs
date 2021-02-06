using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using CollegeRoadApplication.Models;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CollegeRoadApplication.Controllers.Api
{
    public class AccountsController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _context;

        public AccountsController()
        {
            _context = new ApplicationDbContext();
        }

        public AccountsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        
        // POST: /api/accounts  
        [HttpPost]
        [Authorize(Roles = "Admin, SCO")]
        public IHttpActionResult Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Name = model.Name,
                    Gender = model.Gender,
                    ContactNumber = model.ContactNumber,
                    Age = model.Age,
                    UserName = model.UserName,
                    Email = model.Email
                };
                var result = UserManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    //Assign Role to User      
                    this.UserManager.AddToRole(user.Id, model.UserRoles);

                    return CreatedAtRoute("DefaultApi", new { id = user.Id }, model);
                }

            }

            return BadRequest();

        }
    }
}
