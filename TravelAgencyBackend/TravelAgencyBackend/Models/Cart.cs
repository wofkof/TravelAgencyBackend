using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace TravelAgencyBackend.Models
{
    public enum CartCategory
    {
        [Display(Name = "官方細節行程")]
        OfficialTravelDetailId,
        [Display(Name = "客製化行程")]
        CustomTravel
    }
    public enum CartStatus 
    {
        [Display(Name = "已結帳")]
        CheckedOut,
        [Display(Name = "未結帳")]
        Unchecked,
        [Display(Name = "已刪除")]
        Deleted 
    }
    public class Cart
    {
        public int CartId { get; set; }
        public int MemberId { get; set; }
        public int ItemId{ get; set; }
        public CartCategory Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public CartStatus Status { get; set; } = CartStatus.Unchecked;

        public Member Member { get; set; } = null!;
    }
}
