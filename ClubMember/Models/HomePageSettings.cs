using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ClubMember.Models
{
    public class HomePageSettings
    {
        [DisplayName("Background Image")]
        public string BgImage { get; set; }

        [DisplayName("Home Page Name")]
        public string HomePageName { get; set; }

        public HomePageSettings()
        {
            this.HomePageName = "Home Page";
        }

    }
}