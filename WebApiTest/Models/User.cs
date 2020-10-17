using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiTest.Models
{
    public class User
    {
        [Required]
        [Display(Name = "User name")]
        public string Username { get; set; }

        //[Required]
        //[DataType(DataType.EmailAddress)]
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        //[Required]
        //[DataType(DataType.Date)]
        //[Display(Name = "Birthday")]
        //public DateTime Birthday { get; set; }

        //[Required]
        //[Display(Name = "Gender")]
        //public string Gender { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        
    }
}