using System.ComponentModel.DataAnnotations;

namespace E_Commerce_MVC.DTO
{
    public class OrderDataDTO
    {
        [Required]
        public string Adress { get; set; }
        [Required]
        public string phoneNumber { get; set; }

    }
}
