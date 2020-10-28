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
                    Avatar=user.Avatar,
                    FavoriteCount=favoriteCount,
                    ReviewCount=reviewCount
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
                    Avatar = user.Avatar,
                    FavoriteCount = favoriteCount,
                    ReviewCount = reviewCount
                });

            }
        }
    }
}
