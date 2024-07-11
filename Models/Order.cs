using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_MVC.Models
{
    public class Order
    {
        public Guid id { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public Guid OrderDataId { get; set; }
        public OrderData orderData { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public bool Opproved { get; set; } = false;
        
    }
}
