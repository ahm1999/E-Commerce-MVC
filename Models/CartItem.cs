using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_MVC.Models
{
    public class CartItem
    {
       public Guid Id {get; set; }

      
        [ForeignKey("Product")]
        public Guid ProductId {get; set; }

        public Guid CartId { get; set; }
        public int Quantity { get; set; } = 1;
        public Product Product { get; set; }   

    }
}
