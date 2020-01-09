using ClubMember.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
        ObjectCache cacheSlot = MemoryCache.Default;
        List<RegularMember> listRegMembers;

        ApplicationDbContext context = new ApplicationDbContext();

        SqlConnection DbConnect = new SqlConnection();
        SqlCommand DbCommand = new SqlCommand();
        SqlDataReader DbReader;

        public AdminController()
        {
            listRegMembers = cacheSlot["listRegMembers"] as List<RegularMember>;

            if (listRegMembers == null)
            {
                listRegMembers = new List<RegularMember>();
            }
        }

        public void SavecacheSlot()
        {
            cacheSlot["listRegMembers"] = listRegMembers;
        }

        public bool UpdateMemberfromDB(RegularMember member, string id)
        {
            DbConnect.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-ClubMember-20191219041643;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            DbConnect.Open();
            DbCommand.Connection = DbConnect;
            DbCommand.CommandText = "Update dbo.AspNetUsers " +
                                    "set FirstName = '" + member.firstName +
                                     "', LastName = '" + member.lastName +
                                     "', Email = '" + member.MemberEmail +
                                     "', AccStatus = '" + member.AccStatus +
                                     "' where Id = '" + id + "';";

            DbReader = DbCommand.ExecuteReader();

            if (DbReader.RecordsAffected > 0)
                return (true);

            else
                return (false);

        }

        public bool DeleteMemberfromDB(string id)
        {
            DbConnect.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-ClubMember-20191219041643;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            DbConnect.Open();
            DbCommand.Connection = DbConnect;
            DbCommand.CommandText = "Delete from dbo.AspNetUsers where Id = '" + id + "';";
            DbReader = DbCommand.ExecuteReader();

            if (DbReader.RecordsAffected > 0)
                return (true);

            else
                return (false);

        }

        // GET: Admin
        public ActionResult Index()
        {
            return View(context.Users.ToList());
        }

        public ActionResult AddRegularMember()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRegularMember(RegularMember member)
        {
            member.ID = Guid.NewGuid().ToString();
            listRegMembers.Add(member);
            SavecacheSlot();

            return RedirectToAction("Index");
        }

        public ActionResult ViewMemberInfo(string id)
        {
            //Find member with specific ID
            RegularMember member = listRegMembers.FirstOrDefault(c => c.ID == id);

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
            RegularMember member = listRegMembers.FirstOrDefault(c => c.ID == id);

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
        public ActionResult EditMemberInfo(RegularMember member, string id)
        {
            var memberToEdit = listRegMembers.FirstOrDefault(c => c.ID == id);
            //RegularMember memberToEdit = listRegMembers.FirstOrDefault(c => c.ID == id);

            if (memberToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Changes to Cache
                memberToEdit.firstName = member.firstName;
                memberToEdit.lastName = member.lastName;
                memberToEdit.MemberEmail = member.MemberEmail;
                memberToEdit.AccStatus = member.AccStatus;
                SavecacheSlot();

                //Changes to DB
                if (!UpdateMemberfromDB(member, id))
                {
                    return HttpNotFound();
                }

                return RedirectToAction("Index");
            }
        }

        //Show Delete Confirmation Page
        public ActionResult DeleteRegMember(string id)
        {
            RegularMember member = listRegMembers.FirstOrDefault(c => c.ID == id);

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
            RegularMember member = listRegMembers.FirstOrDefault(c => c.ID == id);

            if (member == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Changes to Cache
                listRegMembers.Remove(member);

                //Changes to DB
                if (!DeleteMemberfromDB(id))
                {
                    //return HttpNotFound();
                }
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