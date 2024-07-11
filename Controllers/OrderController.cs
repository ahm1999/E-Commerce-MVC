using E_Commerce_MVC.DTO;
using E_Commerce_MVC.Models;
using E_Commerce_MVC.Services;
using E_Commerce_MVC.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace E_Commerce_MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserManeger _userManeger;
        private readonly ILogger<OrderController> _logger;

        public OrderController(AppDbContext context,IUserManeger userManeger, ILogger<OrderController> logger)
        {
            _context = context;
            _userManeger = userManeger;
            _logger = logger;
        }
        [Authorize]
        public async Task<IActionResult> SubmitOrder()
        {
            var od = await _context.OrderDatas.Where(o => o.UserId == _userManeger.GetUserId()).ToListAsync();
            ToViewOrderDataDTO orderDataDTO = new ToViewOrderDataDTO() { 
            ListOfOrderData = od
            };
            return View(orderDataDTO);
        }
        [Authorize]
        public async Task<IActionResult> AddOrderData([FromForm] OrderDataDTO userData) {

            if (await _context.OrderDatas.AnyAsync(od => od.Adress == userData.Adress && od.phoneNumber == userData.phoneNumber && od.UserId == _userManeger.GetUserId())) {

                return RedirectToAction("SubmitOrder");           
            
            }

            Guid OrderDataId = Guid.NewGuid();
            OrderData od = new OrderData()
            {
                Id = OrderDataId,
                Adress = userData.Adress,
                phoneNumber = userData.phoneNumber,
                UserId = _userManeger.GetUserId()
            };
            await _context.OrderDatas.AddAsync(od);
            await _context.SaveChangesAsync();
            return Redirect("/Order/ProccedOrder/"+OrderDataId.ToString());
        
        }


        [Authorize]
        [Route("[controller]/ProccedOrder/{OrderDataId:guid}")]
        public async Task<IActionResult> ProccedOrder([FromRoute] Guid OrderDataId) {
            _logger.LogInformation(OrderDataId.ToString());

            Guid orderId = Guid.NewGuid();
            Order order = new Order() { 
            id = orderId,
            Opproved = false,
            UserId = _userManeger.GetUserId(),
            OrderDataId = OrderDataId,
            CreatedDate = DateTime.UtcNow
            };
            Cart cart = _context.Carts.Include(c => c.cartItems).FirstOrDefault(c => c.UserId == _userManeger.GetUserId());
            if (cart.cartItems.Count == 0) {
                _logger.LogInformation("cart is empty");
                return RedirectToAction("SubmitOrder");
            }
            var OrderItems =  cart.cartItems.Select((ci) => {
                return new OrderItem()
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity
                };
            });
            
            // _logger.LogInformation(OrderItems.ToJson());
            await _context.OrderItems.AddRangeAsync(OrderItems);
            await _context.Orders.AddAsync(order);
            _context.CartItems.RemoveRange(cart.cartItems);
            await _context.SaveChangesAsync();
            return RedirectToAction("SubmitOrder");
        }
    }
}
