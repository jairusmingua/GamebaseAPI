using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace WebApiTest.Controllers
{
    public class GameController : ApiController
    {
       
        [Route("api/game/topgames")]
        [HttpGet]
        public IEnumerable<Game>GetTopGames()
        {
            List<Game> game;
            using (var context = new gamebase1Entities())
            {

                game = context.Games.Take(10).ToList(); /// get two games from db wala pang rankings ito or whatever

                      

            }

            return game;
        }
        [Route("api/game/favorites")]
        [HttpGet]
        //this must be authenticated but for the mean time gamit muna ng isang user which is 'jai' na nasa db
        public IEnumerable<Game> GetFavorites()
        {
            List<Game> game;
            using (var context = new gamebase1Entities())
            {

                game = (from u in context.User_Credentials_
                        join f in context.Favorites
                        on u.UserID equals f.UserID
                        join g in context.Games
                        on f.GameID equals g.GameID
                        where u.Username == "jai"
                        select g
                        ).ToList();



            }

            return game;

        }
        [Route("api/game/{id:guid}")]
        [HttpGet]
        public IHttpActionResult GetGame(Guid id)
        {   

            using (var context = new gamebase1Entities())
            {
                IQueryable<Game> game = context.Games.Where(p => p.GameID == id);
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
