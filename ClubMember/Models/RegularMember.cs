using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebDesignTest.Models
{
    public class RegularMember
    {
        public string ID { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Your First Name is too long, please check.")]
        [DisplayName("First Name")]
        public string firstName { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Your Last Name is too long, please check.")]
        [DisplayName("Last Name")]
        public string lastName { get; set; }

    }


}