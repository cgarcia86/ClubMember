using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClubMember.Models
{
    public class RegularMember
    {
        public string id { get; set; }

        public string profilePic { get; set; }
        public DateTime BirthDate { get; set; }


        public RegularMember()
        {
           
        }

    }

   


}