using ClubMember;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using WebDesignTest.Models;


namespace WebDesignTest.Controllers
{
    public class RegularMemberController : Controller
    {

       

        public RegularMemberController()
        {
            
        }

       

        // GET: RegularMember
        public ActionResult Index()
        {
            

            return View();

        }

       
    }
}