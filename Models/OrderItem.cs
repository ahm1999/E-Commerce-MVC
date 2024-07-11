using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_MVC.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }


        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        public int Quantity { get; set; } = 1;
        public Product Product { get; set; }
    }
}
