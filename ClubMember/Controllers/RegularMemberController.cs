using ClubMember;
using ClubMember.Models;
using ClubMember.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;


namespace WebDesignTest.Controllers
{
    public class RegularMemberController : Controller
    {

        ApplicationDbContext context = new ApplicationDbContext();

        public RegularMemberController()
        {
            
        }

       

        // GET: RegularMember
        public ActionResult Index()
        {
            

            return View();

        }

        public ActionResult ViewUserProfile(string id)
        {
            var UsrProfile = context.RegUsr.Find(id);
            var UsrInfo = context.Users.Find(id);

            RegUsrProfileViewModel profileViewModel = new RegUsrProfileViewModel
            {
                RegMember = UsrProfile,
                RegUsr = UsrInfo
            };

            if(profileViewModel == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(profileViewModel);
            }
 
        }

       
    }
}