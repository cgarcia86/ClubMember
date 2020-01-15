using ClubMember.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ClubMember.ViewModels
{
    public class RegUsrProfileViewModel
    {
        RegularMember RegMember { get; set; }

        ApplicationUser RegUsr { get; set; }
    }
}