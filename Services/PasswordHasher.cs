using BC = BCrypt.Net.BCrypt;

namespace E_Commerce_MVC.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
           return BC.EnhancedHashPassword(password);
        }

        public bool VerifyPassword(string HashedPassowrd, string userSubmitterdPassowrd)
        {
            return BC.EnhancedVerify(userSubmitterdPassowrd, HashedPassowrd);
        }
    }
}
