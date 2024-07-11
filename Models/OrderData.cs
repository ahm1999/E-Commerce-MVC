using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_MVC.Models
{
    public class OrderData
    {
        public Guid Id { get; set; }
        public string Adress { get; set; }

        public string phoneNumber { get; set; }

        [ForeignKey("Users")]
        public Guid UserId { get; set; }
    }
}
