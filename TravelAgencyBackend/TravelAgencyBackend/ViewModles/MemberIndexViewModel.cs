﻿using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgencyBackend.Models;

namespace TravelAgencyBackend.ViewModles
{
    public class MemberIndexViewModel
    {
        public string? SearchText { get; set; }
        public MemberStatus? FilterStatus { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }
        public IEnumerable<Member> Members { get; set; } = new List<Member>();
    }
}
