using E_Commerce_MVC.DTO;
using E_Commerce_MVC.Models;
using E_Commerce_MVC.Services;
using E_Commerce_MVC.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_MVC.Controllers
{
    public class AuthController : Controller
    {

        private readonly IUserManeger _userManeger;
        private readonly ILogger<AuthController> _logger;
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _hasher;
        public AuthController(IUserManeger userManeger,ILogger<AuthController> logger,AppDbContext context,IPasswordHasher hasher)
        {
            _userManeger = userManeger;
            _hasher = hasher;
            _logger = logger;
            _context = context;
        }

        [Authorize]
        public IActionResult Protected() {

            return  Ok($"this is protected {_userManeger.GetUserId()}");
        }

        [Authorize(Roles ="admin")]
        public IActionResult ProtectedAdmin()
        {

            return Ok($"this is admin protected {_userManeger.GetUserId()}");
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register() {

            return View();
        } 

        [HttpPost]
        public async Task<IActionResult> Sign_up([FromForm] SignUpDTO userData) {

            if(_context.Users.Any(u => u.Email == userData.Email)){
                return Redirect("/auth/Register");

            }
            Guid userId = Guid.NewGuid();
            Guid CartId = Guid.NewGuid();


            User user = new() {
                Id = userId,
                Name = userData.Name,
                Password = _hasher.HashPassword(userData.Password),
                Role = "user",
                Email = userData.Email,
                CartId = CartId

            };
             var Cart = new Cart()
             {
                 Id = CartId,
                 UserId = userId,
                 CreatedDate = DateTime.Now,
                 
                 
             };

            await _context.Users.AddAsync(user);
            await _context.Carts.AddAsync(Cart);
            await _context.SaveChangesAsync();

            return Redirect("/home/Index");
        
        }
        [HttpPost]
        public async Task<IActionResult> SignIn_p([FromForm]SignInDTO userdata ) {

            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userdata.Email);
            if (user == null)
            {
                return Redirect("/auth/SignIn");
            }
            //check the password
            if (!_hasher.VerifyPassword(user.Password, userdata.Password)) {
                return Redirect("/auth/SignIn");
            }
            await _userManeger.SignInUser(user);

            return Redirect("/home/Index");
        }
        [Authorize]
        public async Task<IActionResult> Sign_Out() {

            await _userManeger.SignOutUser();
            return Ok("signOut");
        }
    }
}

