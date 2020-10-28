using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTest.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String FirstName{get;set;}
        public String LastName { get; set; }
        public String Avatar { get; set; }

    }
}