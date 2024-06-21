using E_Commerce_MVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
namespace E_Commerce_MVC.Services
{
    public class UserManeger : IUserManeger
    {   
        private readonly IHttpContextAccessor _accessor;
        public UserManeger(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public Guid GetUserId()
        {
           string Id =  _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;

            return Guid.Parse(Id);
        }

        public async Task <string> SignInUser(User user)
        {
            var claims = new List<Claim>{
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim("FullName", user.Name),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = "http://localhost:5236/home/Index"
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await _accessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity), 
            authProperties);

            return "signed in";
        }


        public async Task< string> SignOutUser()
        {
            await _accessor.HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

            return "signed Out";
        }
    }
}
