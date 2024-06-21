using System.ComponentModel.DataAnnotations;

namespace E_Commerce_MVC.DTO
{
    public class SignUpDTO
    {
        [Required]
        public string Name { get; set; } 
        [Required]
        [EmailAddress]
        public string Email { get; set; } 
        [Required]
        public string Password { get; set; } 

    }
}
