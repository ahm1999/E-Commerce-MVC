using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_MVC.Models
{
    public class Cart
    {
        public Guid Id { get; set; }   

        public DateTime CreatedDate { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }   
        public ICollection<CartItem> cartItems { get; set; }
    }
}
