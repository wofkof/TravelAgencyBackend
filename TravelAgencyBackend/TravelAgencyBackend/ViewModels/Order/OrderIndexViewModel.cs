using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using TravelAgencyBackend.Models;

namespace TravelAgencyBackend.ViewModels.Order
{
    public class OrderIndexViewModel
    {
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public int ItemId { get; set; }
        public OrderCategory Category { get; set; }
        public int ParticipantsCount { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public Member Member { get; set; }
        public OfficialTravel? OfficialTravel { get; set; }  // 加這個
        public CustomTravel? CustomTravel { get; set; }      // 加這個

        public string? DisplayTitle => Category switch
        {
            OrderCategory.OfficialTravelDetail => OfficialTravel?.Title,
            OrderCategory.CustomTravel => CustomTravel?.Note,
            _ => null
        };
    }
}
