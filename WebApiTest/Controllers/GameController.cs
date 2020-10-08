using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace WebApiTest.Controllers
{
    public class GameController : ApiController
    {
        Game[] games = new Game[]
        {
            new Game{GameId=1,GameTitle="Call Of Duty: Ghosts",ImageUrl="https://vignette.wikia.nocookie.net/callofduty/images/6/6f/Call_of_Duty_Ghosts_PC_cover_art.jpg/revision/latest?cb=20130502141835",Developer="Infinity Ward",Synopsis="Sample Lang", MatureRating="PG", ReleaseDate=DateTime.Now},
            new Game{GameId=2,GameTitle="Farcry Primal",ImageUrl="https://vignette.wikia.nocookie.net/callofduty/images/6/6f/Call_of_Duty_Ghosts_PC_cover_art.jpg/revision/latest?cb=20130502141835",Developer="Infinity Ward",Synopsis="Sample Lang", MatureRating="PG", ReleaseDate=DateTime.Now },
            new Game{GameId=3,GameTitle="GTA Sa",ImageUrl="https://upload.wikimedia.org/wikipedia/en/c/c4/GTASABOX.jpg",Developer="Rockstar Games",Synopsis="Sample Lang", MatureRating="PG", ReleaseDate=DateTime.Now }
        };  
        [Route("api/game/topgames")]
        [HttpGet]
        public IEnumerable<Game>GetTopGames()
        {
            List<Game> game;
            using (var context = new gamebasedbEntities())
            {
                game = context.Games.ToList();
            }
            return game;
        }
        [Route("api/game/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetGame(int id)
        {
            using (var context = new gamebasedbEntities())
            {
                IQueryable<Game> game = context.Games.Where(p => p.GameId == id);
                Game g = game.ToList()[0];
                if (g == null)
                {
                    return NotFound();
                }
                return Ok(g);
            }
        }
    }
}
