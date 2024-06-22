using E_Commerce_MVC.Models;
using E_Commerce_MVC.Services;
using E_Commerce_MVC.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserManeger _userManeger;
        public CartController(AppDbContext context, IUserManeger userManeger)
        {
            _context = context;
            _userManeger = userManeger;
        }

        [Authorize]
        [HttpPost]
        [Route("[controller]/RemovefromCart/{cartItemId:guid}")]
        public async Task<IActionResult> RemoveFromCart([FromRoute] Guid cartItemId)
        {


            Cart cart = await _context.Carts.Include(c => c.cartItems)
                                             .FirstOrDefaultAsync(u => u.UserId == _userManeger.GetUserId());
            _context.CartItems.Remove(cart.cartItems.FirstOrDefault(ci => ci.Id == cartItemId));

            await _context.SaveChangesAsync();
            return Redirect("/cart/GetCart");




        }


        [Authorize]
        public async Task<IActionResult> GetCart()
        {
            var cart = await _context.Carts.Include(c => c.cartItems)
                                           .ThenInclude(c => c.Product)
                                           .FirstOrDefaultAsync(c => c.UserId == _userManeger.GetUserId());



            return View(cart);
        }

        [Authorize]
        [HttpPost]
        [Route("[controller]/AddToCart/{ProductId:guid}")]

        public async Task<IActionResult> AddToCart([FromRoute] Guid ProductId)
        {

            if (!await _context.Products.AnyAsync(p => p.Id == ProductId))
            {
                return BadRequest();
            }


            var cart = await _context.Carts.FirstOrDefaultAsync(u => u.UserId == _userManeger.GetUserId());

            Guid cartId = cart.Id;

            CartItem cartItem = new CartItem()
            {
                Id = Guid.NewGuid(),
                ProductId = ProductId,
                CartId = cartId

            };
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
