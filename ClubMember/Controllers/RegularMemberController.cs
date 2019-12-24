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

        ObjectCache cacheSlot = MemoryCache.Default;
        List<RegularMember> listRegMembers;

        SqlConnection DbConnect = new SqlConnection();
        SqlCommand DbCommand = new SqlCommand();
        SqlDataReader DbReader;

        

        public RegularMemberController()
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

        //Get User List from Data Base
        public void LoadUserListfromDB()
        {
            DbConnect.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-ClubMember-20191219041643;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            DbConnect.Open();
            DbCommand.Connection = DbConnect;
            DbCommand.CommandText = "Select Id, FirstName, LastName from dbo.AspNetUsers";
            DbReader = DbCommand.ExecuteReader();

            if(DbReader.HasRows)
            {
                
                while(DbReader.Read())
                {
                    RegularMember member = new RegularMember();

                    member.ID = DbReader.GetString(0);
                    member.firstName = DbReader.GetString(1);
                    member.lastName = DbReader.GetString(2);

                    //Saver member Info to Cache
                    listRegMembers.Add(member);
                    cacheSlot["listRegMembers"] = listRegMembers;
                }
                
            }
            //else
            //{
            //    RedirectToAction("Error");
            //    Console.WriteLine("Error");
            //}
        }

        public bool UpdateMemberfromDB(RegularMember member, string id)
        {
            DbConnect.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-ClubMember-20191219041643;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            DbConnect.Open();
            DbCommand.Connection = DbConnect;
            DbCommand.CommandText = "Update dbo.AspNetUsers " +
                "                    set FirstName = '" + member.firstName +
                                     "', LastName = '"+ member.lastName +"' where Id = '"+id +"';";
            DbReader = DbCommand.ExecuteReader();

            if (DbReader.Read())
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }

        public bool DeleteMemberfromDB(string id)
        {
            DbConnect.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-ClubMember-20191219041643;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            DbConnect.Open();
            DbCommand.Connection = DbConnect;
            DbCommand.CommandText = "Delete from dbo.AspNetUsers where Id = '" + id + "';";
            DbReader = DbCommand.ExecuteReader();

            if (DbReader.Read())
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
        // GET: RegularMember
        public ActionResult Index()
        {
            if (cacheSlot["listRegMembers"] == null)
                LoadUserListfromDB();

            return View(listRegMembers);

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


        //Member List
        public ActionResult MemberList()
        {
            return View();
        }
    }
}