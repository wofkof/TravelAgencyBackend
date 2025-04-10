using TravelAgencyBackend.Models;

namespace TravelAgencyBackend.ViewModels
{
    public class CartKeyWordViewModel
    {
        public string? txtKeyword { get; set; }
        public string? Category { get; set; }
        public string? Status { get; set; }
        public List<Cart> Carts { get; set; } 
    }
}
