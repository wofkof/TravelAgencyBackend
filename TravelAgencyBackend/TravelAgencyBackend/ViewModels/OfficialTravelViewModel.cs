using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TravelAgencyBackend.ViewModels // ← 修改成正確拼法
{
    public enum TravelStatus
    {
        [Display(Name = "未上架")]
        Draft,
        [Display(Name = "上架中")]
        Published,
        [Display(Name = "已下架")]
        Unavailable
    }

    public class OfficialTravelViewModel
    {
        public int CreatedByEmployeeId { get; set; }

        public int RegionId { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public int ProjectYear { get; set; }

        public DateTime AvailableFrom { get; set; }

        public DateTime AvailableUntil { get; set; }

        [Required]
        public string Description { get; set; } = null!;

        public int Days { get; set; }

        [Display(Name = "封面圖片")]
        [Required(ErrorMessage = "請選擇封面圖片")]
        public IFormFile? CoverImage { get; set; }

        public TravelStatus Status { get; set; }
    }
}
