namespace TravelAgencyBackend.ViewModels.Cart
{
    public class CartKeyWordViewModel
    {
        public string? txtKeyword { get; set; }
        public string? Category { get; set; }
        public string? Status { get; set; }
        public List<Models.Cart> Carts { get; set; } = new List<Models.Cart>();
    }
}
