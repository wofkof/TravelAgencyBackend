﻿using System.ComponentModel.DataAnnotations;

namespace TravelAgencyBackend.Models
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
    public class OfficialTravel
    {
        public int OfficialTravelId { get; set; }
        [Display(Name = "負責人")]
        public int CreatedByEmployeeId { get; set; }
        [Display(Name = "地區/縣市")]
        public int RegionId { get; set; }
        [Display(Name = "專案名稱")]
        public string Title { get; set; } = null!;
        [Display(Name = "專案年分")]
        public int ProjectYear { get; set; }
        [Display(Name = "上架日期")]
        public DateTime AvailableFrom { get; set; }
        [Display(Name = "下架日期")]
        public DateTime AvailableUntil { get; set; }
        [Display(Name = "專案描述")]
        public string Description { get; set; } = null!;
        [Display(Name = "行程時長")]
        public int Days { get; set; }
        [Display(Name = "封面圖片")]
        public string CoverPath { get; set; } = null!;
        [Display(Name = "創建日期")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "最新更新")]
        public DateTime? UpdatedAt { get; set; }
        [Display(Name = "狀態")]
        public TravelStatus Status { get; set; } = TravelStatus.Draft;

        public ICollection<OfficialTravelDetail> Details { get; set; } = new List<OfficialTravelDetail>();
        [Display(Name = "負責人")]
        public Employee CreatedBy { get; set; } = null!;
        [Display(Name = "國家")]
        public Region Region { get; set; } = null!;
    }
}
