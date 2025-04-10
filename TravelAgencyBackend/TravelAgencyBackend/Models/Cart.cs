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

        [Display(Name = "會員編號")]
        public int MemberId { get; set; }

        [Display(Name = "商品編號")]
        public int ItemId{ get; set; }

        [Display(Name = "類別")]
        public CartCategory Category { get; set; }

        [Display(Name = "建立時間")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "狀態")]
        public CartStatus Status { get; set; } = CartStatus.Unchecked;

        [Display(Name = "會員")]
        public Member Member { get; set; } = null!;
    }
}
