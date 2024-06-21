using E_Commerce_MVC.Models;

namespace E_Commerce_MVC.Services
{
    public interface IUserManeger
    {
        public Task <string> SignInUser(User user);

        public Task<string> SignOutUser ();

        public Guid GetUserId();
    }
}
