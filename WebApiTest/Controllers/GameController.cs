using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiTest.Models;

namespace WebApiTest.Controllers
{
    public class GameController : ApiController
    {
        Game[] games = new Game[]
        {
            new Game{GameId=1,ImageUrl="https://vignette.wikia.nocookie.net/callofduty/images/6/6f/Call_of_Duty_Ghosts_PC_cover_art.jpg/revision/latest?cb=20130502141835",Developer="Infinity Ward",Synopsis="Sample Lang", MatureRating="PG", ReleaseDate=DateTime.Now},
            new Game{GameId=2,GameTitle="Farcry",ImageUrl="https://vignette.wikia.nocookie.net/callofduty/images/6/6f/Call_of_Duty_Ghosts_PC_cover_art.jpg/revision/latest?cb=20130502141835",Developer="Infinity Ward",Synopsis="Sample Lang", MatureRating="PG", ReleaseDate=DateTime.Now },
            new Game{GameId=3,GameTitle="Gta",ImageUrl="https://vignette.wikia.nocookie.net/callofduty/images/6/6f/Call_of_Duty_Ghosts_PC_cover_art.jpg/revision/latest?cb=20130502141835",Developer="Infinity Ward",Synopsis="Sample Lang", MatureRating="PG", ReleaseDate=DateTime.Now }
        };  
        [Route("api/game/topgames")]
        [HttpGet]
        public IEnumerable<Game>GetTopGames()
        {
            return games;
        }
        [Route("api/game/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetGame(int id)
        {
            var game = games.FirstOrDefault((p) => p.GameId == id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }
    }
}
