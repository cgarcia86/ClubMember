using ClubMember.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ClubMember.ViewModels
{
    public class RegUsrProfileViewModel
    {
        public RegularMember RegMember { get; set; }

        public ApplicationUser RegUsr { get; set; }
    }
}