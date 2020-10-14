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
