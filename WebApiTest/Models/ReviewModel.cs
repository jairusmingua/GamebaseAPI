using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTest.Models
{
    public class ReviewModel
        //custom review model 
    {
        public System.Guid ReviewID { get; set; }
        public Nullable<System.Guid> GameID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public Game Game { get; set; }
        public string ReviewText { get; set; }
        public int StarRating { get; set; }


    }
}