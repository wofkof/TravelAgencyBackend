using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using TravelAgencyBackend.Models;

namespace TravelAgencyBackend.ViewModels.Order
{
    public class OrderCreateViewModel
    {
        [Required(ErrorMessage = "請選擇會員")] //會員
        public int MemberId { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required(ErrorMessage = "請選擇訂單類型")]
        public OrderCategory Category { get; set; }
        [Required(ErrorMessage = "請選擇參與者")]
        public int ParticipantId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "出團人數至少為 1")]
        public int ParticipantsCount { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "總金額必須大於 0")]
        public decimal TotalAmount { get; set; }
        [Required(ErrorMessage = "請選擇狀態")]
        public OrderStatus Status { get; set; }
        [Required(ErrorMessage = "請選擇付款方式")]
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Note { get; set; }
        public SelectList? OfficialTravels { get; set; } // Add this
        public SelectList? CustomTravels { get; set; }    // Add this
    }
}