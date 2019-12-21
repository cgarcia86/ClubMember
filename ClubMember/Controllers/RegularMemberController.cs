using System;
using System.Collections.Generic;
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
        // GET: RegularMember
        public ActionResult Index()
        {

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
                memberToEdit.firstName = member.firstName;
                memberToEdit.lastName = member.lastName;
                SavecacheSlot();

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
                listRegMembers.Remove(member);
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