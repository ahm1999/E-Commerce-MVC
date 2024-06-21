

namespace E_Commerce_MVC.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;

        public int Price { get; set; }
        public DateTime CreatedOn { get; set; }


        public ICollection<Category> categories { get; set; }


    }
}