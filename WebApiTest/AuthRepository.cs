using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WebApiTest.Models;

namespace WebApiTest
{
    //don't touch thiss heheeh 
    public class AuthRepository :IDisposable
    {
        private AuthContext _ctx;

        private UserManager<ApplicationUser> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(User userModel)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.Username,
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Avatar = userModel.Avatar
               
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }
        public async Task<IdentityResult> ChangePassword(ClaimsIdentity identity,ChangeValidationModel passwordModel)
        {
            using(var context =new gamebase1Entities()){

         
                var claims = from c in identity.Claims //extracting the username in var identity
                             select new
                             {
                                 subject = c.Subject.Name,
                                 type = c.Type,
                                 value = c.Value
                             };
                var userName = claims.ToList()[0].value.ToString(); //converting to string 
                AspNetUser user = context.AspNetUsers.Where(u => u.UserName == userName).Single();


                var result = await _userManager.ChangePasswordAsync(user.Id, passwordModel.OldPassword, passwordModel.NewPassword);

                return result;
            }
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
    
}