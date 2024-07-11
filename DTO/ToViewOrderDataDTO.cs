using E_Commerce_MVC.Models;

namespace E_Commerce_MVC.DTO
{
    public class ToViewOrderDataDTO
    {
        public string Adress { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<OrderData> ListOfOrderData { get; set; }
    }
}
