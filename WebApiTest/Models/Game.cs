using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTest.Models
{
    public class Game
    {
        public System.Guid GameID { get; set; }
        public string GameTitle { get; set; }
        public string GameImageURL { get; set; }
        public string Synposis { get; set; }
        public string GameReleased { get; set; }
        public string MatureRating { get; set; }
        public string Developer { get; set; }
    }
}