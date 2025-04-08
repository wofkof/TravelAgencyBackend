namespace TravelAgencyBackend.ViewModles
{
    public class CartItemViewModel
    {
        public int CartId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }//資料庫無此欄位
        public decimal Subtotal => UnitPrice * Quantity;
        public string CategoryName { get; set; } = string.Empty;
    }
}
