using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTest.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public String GameTitle { get; set; }
        public String ImageUrl { get; set; }
        public String Synopsis { get; set; }
        public DateTime ReleaseDate { get; set; }
        public String MatureRating { get; set; }
        public String Developer { get; set; }
        
    }
}