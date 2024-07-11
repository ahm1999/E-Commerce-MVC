using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_MVC.Models
{
    public class User
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;

        public string Role { get; set; } = String.Empty;

        [ForeignKey("Cart")]
        public Guid CartId  { get; set; }
        public Cart  Cart{ get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
