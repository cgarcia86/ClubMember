using ClubMember.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using WebDesignTest.Models;

namespace ClubMember.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminController : Controller
    {
       
        ApplicationDbContext context = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            var usersList = context.Users;
            return View(usersList);
        }

        public ActionResult AddRegularMember()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRegularMember(RegularMember member)
        {
            member.ID = Guid.NewGuid().ToString();
            //listRegMembers.Add(member);
            //SavecacheSlot();

            return RedirectToAction("Index");
        }

        public ActionResult ViewMemberInfo(string id)
        {
            //Find member with specific ID
            ApplicationUser member = context.Users.Find(id);

            if (member == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(member);
            }
            
        }

        public ActionResult EditMemberInfo(string id)
        {

            ApplicationUser member = context.Users.Find(id);

            if (member == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(member);
            }
        }

        [HttpPost]
        public ActionResult EditMemberInfo(ApplicationUser member, string id)
        {
            
           ApplicationUser memberToEdit = context.Users.Find(id);

            if (memberToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {

                memberToEdit.FirstName = member.FirstName;
                memberToEdit.LastName = member.LastName;
                memberToEdit.Email = member.Email;
                memberToEdit.AccStatus = member.AccStatus;

                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        //Show Delete Confirmation Page
        public ActionResult DeleteRegMember(string id)
        {
            ApplicationUser member = context.Users.Find(id);

            if (member == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(member);
            }

        }

        [HttpPost]
        [ActionName("DeleteRegMember")]
        //Delete Regular Member
        public ActionResult ConfirmDeleteRegMember(string id)
        {
            ApplicationUser memberToDelete = context.Users.Find(id);

            if (memberToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Users.Remove(memberToDelete);
                context.SaveChanges();
                return RedirectToAction("Index");
               
            }
        }

        public ActionResult AssignRole()
        {
            ViewBag.Roles = context.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AssignRole(FormCollection form)
        {
           
            string usrName = form["UserNameTxt"];
            string usrRole = form["RoleName"];

            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(usrName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            userManager.AddToRole(user.Id, usrRole);

            return RedirectToAction("Index", "Home");

        }

    }
}