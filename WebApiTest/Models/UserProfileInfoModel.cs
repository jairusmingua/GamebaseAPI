using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTest.Models
{
    public class UserProfileInfoModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set;}
        public int FavoriteCount { get; set; }
        public int ReviewCount { get; set; }

    }
}