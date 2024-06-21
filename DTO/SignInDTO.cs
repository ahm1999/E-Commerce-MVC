using System.ComponentModel.DataAnnotations;

namespace E_Commerce_MVC.DTO
{
    public class SignInDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } 
        [Required]
        public string Password { get; set; } 
    }
}
