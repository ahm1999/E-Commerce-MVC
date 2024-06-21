namespace E_Commerce_MVC.Services
{
    public interface IPasswordHasher
    {
        public string HashPassword(string password);

        public bool VerifyPassword(string HashedPassowrd,string userSubmitterdPassowrd);    
    }
}
