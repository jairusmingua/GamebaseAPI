using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using WebApiTest.Models;

namespace WebApiTest.Controllers
{
    public class GameController : ApiController
    {
       
        [Route("api/game/topgames")]
        [HttpGet]
        //gets first top 10 from the db
        public IEnumerable<Game>GetTopGames()
        {
            List<Game> game;
            using (var context = new gamebase1Entities())
            {

                game = context.Games.Take(10).ToList(); /// get two games from db wala pang rankings ito or whatever

                      

            }

            return game;
        }
        [Authorize]
        [Route("api/game/favorites")]
        [HttpGet]
        //an authorized request wherein it maches users favorite game
        public IEnumerable<Game> GetFavorites()
        {
            List<Game> game;
            using (var context = new gamebase1Entities())
            {
                var identity = User.Identity as ClaimsIdentity; //each authorized request merong username na nakaattach sa mga request so need natin i extract mga yun at i match sa db
                var claims = from c in identity.Claims //extracting the username in var identity
                             select new
                             {
                                 subject = c.Subject.Name,
                                 type = c.Type,
                                 value = c.Value
                             };
                var userName = claims.ToList()[0].value.ToString(); //converting to string 
                game = (from u in context.AspNetUsers 
                        join f in context.Favorites
                        on u.Id equals f.UserID
                        join g in context.Games
                        on f.GameID equals g.GameID
                        where u.UserName == userName
                        select g
                        ).ToList();
                // this performs a join command wherein it searches favorite table games that favorite by the username


            }

            return game;

        }
        
        [Authorize]
        [Route("api/game/favorite/{gameId:guid}")]
        [HttpPost]
        //lets you do a authorized command wherein you can favorite a game using its gameid
        public IHttpActionResult SetAsFavorite(Guid gameId)
        {
            using(var context = new gamebase1Entities())
            {
                var identity = User.Identity as ClaimsIdentity;//each authorized request merong username na nakaattach sa mga request so need natin i extract mga yun at i match sa db
                var claims = from c in identity.Claims //extracting the username in var identity
                             select new
                             {
                                 subject = c.Subject.Name,
                                 type = c.Type,
                                 value = c.Value
                             };
                var userName = claims.ToList()[0].value.ToString(); //converting to string 
                AspNetUser user = context.AspNetUsers.Where(u => u.UserName == userName).Single();
                Favorite favorite = new Favorite {
                    FavoriteID = new Guid(),
                    GameID = gameId,
                    UserID = user.Id

                };
                context.Favorites.Add(favorite); //performing transaction
                context.SaveChanges(); //saving to db 
            }
            return Ok();
        }
        [AllowAnonymous]
        [Route("api/game/{id:guid}")]
        [HttpGet]
        //gets game information
        public IHttpActionResult GetGame(Guid id)
        {   

            using (var context = new gamebase1Entities())
            {
                var identity = User.Identity as ClaimsIdentity;
                var claims = from c in identity.Claims //extracting the username in var identity
                             select new
                             {
                                 subject = c.Subject.Name,
                                 type = c.Type,
                                 value = c.Value
                             };
                try
                {
                    var userName = claims.ToList()[0].value.ToString(); //converting to string 
                    AspNetUser user = context.AspNetUsers.Where(u => u.UserName == userName).Single();

                    if (identity.IsAuthenticated)
                    {
                        Game game = context.Games.Where(p => p.GameID == id).Single();
                        Favorite favorite = context.Favorites.Where(u => u.GameID == game.GameID && u.UserID == user.Id).SingleOrDefault();
                        GameModel g = new GameModel
                        {
                            GameID = game.GameID,
                            GameTitle = game.GameTitle,
                            GameImageURL = game.GameImageURL,
                            GameReleased = game.GameReleased,
                            Developer = game.Developer,
                            MatureRating = game.MatureRating,
                            Synopsis = game.Synopsis,
                            isFavorite = favorite != null ? true : false
                        };
                        if (g == null)
                        {
                            return NotFound();
                        }
                        return Ok(g);
                    }
                    else
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
                catch(Exception err)
                {
                    IQueryable<Game> game = context.Games.Where(p => p.GameID == id);
                    Game g = game.ToList()[0];
                    if (g == null)
                    {
                        return NotFound();
                    }
                    return Ok(g);
                }
                //if not authorized use this return 
   
            }            
        }
    }
}
