using E_Commerce_MVC.DTO;
using E_Commerce_MVC.Models;
using E_Commerce_MVC.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace E_Commerce_MVC.Controllers
{
    
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly AppDbContext _context;

        public ProductController(ILogger<ProductController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize(Roles ="admin")]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddProduct_p([FromForm] ProductDTO userData) {

            Product product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = userData.Name,
                Description = userData.Description,
                Price = userData.Price,
                CreatedOn = DateTime.UtcNow
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Redirect("/product/AddProduct");

        }

        [Route("[controller]/Id/{Id:guid}")]
        public async Task<IActionResult> Id([FromRoute] Guid Id) {
            var Product =  await _context.Products.FirstOrDefaultAsync(x => x.Id == Id);
           return View(Product);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}