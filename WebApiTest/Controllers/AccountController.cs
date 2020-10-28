using Microsoft.AspNet.Identity;
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
    [RoutePrefix("api/Account")]
    //this controller can contain application registration, change password etc.
    public class AccountController : ApiController
    {
        private AuthRepository _repo = null;

        public AccountController()
        {
            _repo = new AuthRepository();
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(User userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repo.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }
        [Route("Password")]
        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> ChangePassword(ChangeValidationModel password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repo.ChangePassword(User.Identity as ClaimsIdentity,password);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }
      
        [Route("Avatar")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult ChangeAvatar(string avatar)
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
                user.Avatar = avatar;
                context.SaveChanges();
                return Ok();
           
            }

        }
        [Route("Name")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult ChangeName(User name)
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
                if (name.FirstName != null)
                {
                    user.FirstName = name.FirstName;
                }
                if(name.LastName != null)
                {
                    user.LastName = name.LastName;
                }
                context.SaveChanges();
                return Ok();

            }

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
