using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WebApiTest.Models;

namespace WebApiTest.Controllers
{
    public class UserController : ApiController
    {
        
        [Route("api/user/")]
        [HttpGet]
        public IHttpActionResult GetAuthenticatedUserInfo()
        {
            using (var context = new gamebase1Entities())
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
                int favoriteCount = context.Favorites.Where(u => u.UserID == user.Id).Count();
                int reviewCount = context.Reviews.Where(u => u.UserID == user.Id).Count();
                return Ok(new UserProfileInfoModel { 
                    FirstName = user.FirstName,
                    LastName=user.LastName, 
                    UserName = user.UserName,
                    Avatar=user.Avatar,
                    FavoriteCount=favoriteCount,
                    ReviewCount=reviewCount,
                    UserId = user.Id
                   
                });
              
            }
        }
        [Route("api/user/{userId}")]
        [HttpGet]
        public IHttpActionResult GetUser(string userId)
        {
            using (var context = new gamebase1Entities())
            {
       
                AspNetUser user = context.AspNetUsers.Where(u => u.Id==userId).Single();
                int favoriteCount = context.Favorites.Where(u => u.UserID == user.Id).Count();
                int reviewCount = context.Reviews.Where(u => u.UserID == user.Id).Count();
                return Ok(new UserProfileInfoModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Avatar = user.Avatar,
                    FavoriteCount = favoriteCount,
                    ReviewCount = reviewCount,
                    UserId = user.Id
                    
                });

            }
        }
        [Route("api/user/favorites/{userId}")]
        [HttpGet]
        //an authorized request wherein it maches users favorite game
        public IEnumerable<Game> GetFavorites(string userId)
        {
            List<Game> game;
            using (var context = new gamebase1Entities())
            {
                game = (from u in context.AspNetUsers
                        join f in context.Favorites
                        on u.Id equals f.UserID
                        join g in context.Games
                        on f.GameID equals g.GameID
                        where u.Id==userId 
                        orderby f.DateFavorite descending
                        select g
                        ).ToList();
                // this performs a join command wherein it searches favorite table games that favorite by the username


            }

            return game;

        }
        [Route("api/user/reviews/{userId}")]
        [HttpGet]
        public IEnumerable<ReviewModel> GetUserReviews(string userId)
        {
            using (var context = new gamebase1Entities())
            {

                IQueryable<Review> reviews = context.Reviews.Where(f => f.UserID == userId).OrderByDescending(s=>s.DateReview);
                //Convert to a new model kasi exposed yung password ng user pag ginamit yung default review model
                IQueryable<ReviewModel> r = reviews.Select(s => new ReviewModel
                {
                    ReviewID = s.ReviewID,
                    GameID = s.GameID,
                    Game = context.Games.Where(u => u.GameID == s.GameID).FirstOrDefault(),
                    UserID = s.UserID,
                    UserName = s.AspNetUser.UserName,
                    ReviewText = s.ReviewText,
                    StarRating = s.StarRating,
                    DateReview = s.DateReview
                });
                return r.ToList();

            }

        }
    }
}
