using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClubMember.Models
{
    public class RegularMember
    {

        [Key]
        //[ForeignKey("Id")]
        public string Usrid { get; set; }

        public string profilePic { get; set; }
        public Nullable<DateTime> BirthDate { get; set; }


        public RegularMember()
        {
           
        }

    }

   


}