using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTest.Models
{
    public class GameModel
    {
       
    
        public System.Guid GameID { get; set; }
        public string GameTitle { get; set; }
        public string MatureRating { get; set; }
        public string Developer { get; set; }
        public string Synopsis { get; set; }
        public Nullable<System.DateTime> GameReleased { get; set; }
        public string GameImageURL { get; set; }

        public bool isFavorite { get; set; }
       
    }
}