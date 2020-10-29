using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiTest.Models;

namespace WebApiTest.Controllers
{
    public class ReviewController : ApiController
    {
        [Authorize]
        [HttpPost]
        [Route("api/review/{gameId:guid}")]
        //post a review to a gameid given user is identified
        public IHttpActionResult PostReview(Guid gameId,[FromBody]Review review)
        {
            using(var context = new gamebase1Entities())
            {
                try
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
                    review.GameID = gameId;
                    review.UserID = user.Id;
                    review.DateReview = DateTime.Now;
                    review.ReviewID = Guid.NewGuid();
                    context.Reviews.Add(review);
                    context.SaveChanges();

                }
                catch(Exception err)
                {
                    BadRequest(err.Message.ToString());
                }
            }
            return Ok();
        }
  
        [HttpGet]
        [Route("api/review/{gameId:guid}")]
        //gets a reviews to a gameid  
        public IEnumerable<ReviewModel> GetReview(Guid gameId)
        {
            using (var context = new gamebase1Entities())
            {
               
                IQueryable<Review> reviews = context.Reviews.Where(f=>f.GameID==gameId);
                //Convert to a new model kasi exposed yung password ng user pag ginamit yung default review model
                IQueryable<ReviewModel> r = reviews.Select(s => new ReviewModel { 
                    ReviewID = s.ReviewID,
                    GameID = s.GameID,
                    Game = context.Games.Where(u=>u.GameID==gameId).FirstOrDefault(),
                    UserID = s.UserID,
                    UserName = s.AspNetUser.UserName,
                    ReviewText =s.ReviewText,
                    StarRating = s.StarRating,
                    DateReview = s.DateReview
                    
                });
                return r.ToList() ;
            }
        }
    
    }
}
