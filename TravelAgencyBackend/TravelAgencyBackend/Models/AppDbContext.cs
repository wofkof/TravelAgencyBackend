using Microsoft.EntityFrameworkCore;

namespace TravelAgencyBackend.Models
{
    // Code First
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
              : base(options) { }
        // 宣告註冊
        public DbSet<Member> Members { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<OfficialTravel> OfficialTravels { get; set; }
        public DbSet<OfficialTravelDetail> OfficialTravelDetails { get; set; }
        public DbSet<OfficialTravelSchedule> OfficialTravelSchedules { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<TravelSupplier> TravelSuppliers { get; set; }
        public DbSet<CustomTravel> CustomTravels { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Transportation> Transportations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Attraction> Attractions { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<TravelRecord> TravelRecords { get; set; }

        // Fluent API 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //T_Member
            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("T_Member");
                entity.HasKey(m => m.MemberId);

                entity.Property(m => m.Password).IsRequired().HasMaxLength(100);
                entity.Property(m => m.Name).IsRequired().HasMaxLength(50);
                entity.Property(m => m.CreatedAt).HasColumnType("datetime");
                entity.Property(m => m.UpdatedAt).HasColumnType("datetime");
                entity.Property(m => m.Note).HasMaxLength(255);

                entity.Property(m => m.Account).IsRequired().HasMaxLength(100);
                entity.HasIndex(m => m.Account).IsUnique();
                entity.Property(m => m.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(m => m.Email).IsUnique();
                entity.Property(m => m.Phone).IsRequired().HasMaxLength(20);
                entity.HasIndex(m => m.Phone).IsUnique();

                entity.Property(m => m.Status)
                       .HasConversion<string>()
                       .HasMaxLength(20)
                       .IsRequired();
            });

            // T_Employee
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("T_Employee");
                entity.HasKey(e => e.EmployeeId);
                entity.Property(e => e.RoleId).IsRequired();

                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(100);
                entity.Property(e => e.BirthDate).HasColumnType("date");
                entity.Property(e => e.HireDate).HasColumnType("date");
                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => e.Phone).IsUnique();

                entity.Property(e => e.Status)
                      .HasConversion<string>()
                      .HasMaxLength(20)
                      .IsRequired();

                entity.HasOne(e => e.Role)
                      .WithMany(r => r.Employees)
                      .HasForeignKey(e => e.RoleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // T_Participant
            modelBuilder.Entity<Participant>(entity =>
            {
                entity.ToTable("T_Participant");
                entity.HasKey(p => p.ParticipantId);

                entity.Property(p => p.Name).IsRequired().HasMaxLength(50);
                entity.Property(p => p.BirthDate).HasColumnType("date");
                entity.Property(p => p.EnglishName).HasMaxLength(100);
                entity.Property(p => p.IssuedPlace).HasMaxLength(50);
                entity.Property(p => p.PassportIssueDate).HasColumnType("date");

                entity.Property(p => p.Phone).HasMaxLength(20);
                entity.HasIndex(p => p.Phone).IsUnique();
                entity.Property(p => p.IdNumber).IsRequired().HasMaxLength(20);
                entity.HasIndex(p => p.IdNumber).IsUnique();
                entity.Property(p => p.PassportNumber).HasMaxLength(20);
                entity.HasIndex(p => p.PassportNumber).IsUnique();

                entity.Property(p => p.Gender)
                      .HasConversion<string>()
                      .HasMaxLength(20)
                      .IsRequired();

                entity.HasOne(p => p.Member)
                      .WithMany(m => m.Participants)
                      .HasForeignKey(p => p.MemberId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // T_Cart
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("T_Cart");
                entity.HasKey(c => c.CartId);
                entity.Property(c => c.ItemId).IsRequired();

                entity.Property(c => c.CreatedAt).HasColumnType("datetime");

                entity.Property(c => c.Category)
                      .HasConversion<string>()
                      .HasMaxLength(20)
                      .IsRequired();
                entity.Property(c => c.Status)
                      .HasConversion<string>()
                      .HasMaxLength(20)
                      .IsRequired();

                entity.HasOne(c => c.Member)
                        .WithMany(m => m.Carts)
                        .HasForeignKey(c => c.MemberId)
                        .OnDelete(DeleteBehavior.Restrict);
            });

            // T_Role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("T_Role");
                entity.HasKey(r => r.RoleId);

                entity.Property(r => r.RoleName).IsRequired().HasMaxLength(50);
            });

            // T_Permission
            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("T_Permission");
                entity.HasKey(p => p.PermissionId);

                entity.Property(p => p.PermissionName).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Caption).HasMaxLength(100);
            });

            // M_RolePermission
            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.ToTable("M_RolePermission");
                entity.HasKey(rp => new { rp.RoleId, rp.PermissionId });
                entity.Property(rp => rp.CreatedAt).HasColumnType("datetime");

                entity.HasOne(rp => rp.Role)
                      .WithMany(r => r.RolePermissions)
                      .HasForeignKey(rp => rp.RoleId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(rp => rp.Permission)
                      .WithMany(p => p.RolePermissions)
                      .HasForeignKey(rp => rp.PermissionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // T_ChatRoom
            modelBuilder.Entity<ChatRoom>(entity =>
            {
                entity.ToTable("T_ChatRoom");
                entity.HasKey(c => c.ChatRoomId);
                entity.Property(c => c.CreatedAt).HasColumnType("datetime");
                
                entity.Property(c => c.Status)
                      .HasConversion<string>()
                      .HasMaxLength(20)
                      .IsRequired();

                entity.HasOne(c => c.Employee)
                      .WithMany(e => e.ChatRooms)
                      .HasForeignKey(c => c.EmployeeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Member)
                      .WithOne(m => m.ChatRoom)
                      .HasForeignKey<ChatRoom>(c => c.MemberId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // T_Message
            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("T_Message");
                entity.HasKey(m => m.MessageId);
                entity.Property(m => m.SenderId).IsRequired();
                entity.Property(m => m.SentAt).HasColumnType("datetime");
                entity.Property(m => m.IsRead).IsRequired();
                entity.Property(m => m.Content).IsRequired().HasMaxLength(1000);

                entity.Property(m => m.SenderType)
                      .HasConversion<string>()
                      .HasMaxLength(20)
                      .IsRequired();

                entity.HasOne(m => m.ChatRoom)
                        .WithMany(c => c.Messages)
                        .HasForeignKey(m => m.ChatRoomId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            // T_Announcement
            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.ToTable("T_Announcement");
                entity.HasKey(a => a.AnnouncementId);
                
                entity.Property(a => a.Title).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Content).IsRequired().HasMaxLength(1000);
                entity.Property(a => a.SentAt).HasColumnType("datetime");

                entity.Property(a => a.Status)
                      .HasConversion<string>()
                      .HasMaxLength(20)
                      .IsRequired();

                entity.HasOne(a => a.Employee)
                      .WithMany(e => e.Announcements)
                      .HasForeignKey(a => a.EmployeeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            // T_OfficialTravel
            modelBuilder.Entity<OfficialTravel>(entity =>
            {
                entity.ToTable("T_OfficialTravel");
                entity.HasKey(o => o.OfficialTravelId);
                
                entity.Property(o => o.Title).IsRequired().HasMaxLength(100);
                entity.Property(o => o.ProjectYear).IsRequired();
                entity.Property(o => o.AvailableFrom).HasColumnType("date");
                entity.Property(o => o.AvailableUntil).HasColumnType("date");
                entity.Property(o => o.Description).HasMaxLength(1000);
                entity.Property(o => o.Days).IsRequired();
                entity.Property(o => o.CoverPath).HasMaxLength(200);
                entity.Property(o => o.CreatedAt).HasColumnType("datetime");
                entity.Property(o => o.UpdatedAt).HasColumnType("datetime");

                entity.Property(o => o.Status)
                      .HasConversion<string>()
                      .HasMaxLength(20)
                      .IsRequired();

                entity.HasOne(o => o.CreatedBy)
                        .WithMany(e => e.OfficialTravels)
                        .HasForeignKey(t => t.CreatedByEmployeeId)
                        .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(o => o.Region)
                        .WithMany(e => e.OfficialTravels)
                        .HasForeignKey(t => t.RegionId)
                        .OnDelete(DeleteBehavior.Restrict);
            });

            // T_OfficialTravelDetail
            modelBuilder.Entity<OfficialTravelDetail>(entity =>
            {
                entity.ToTable("T_OfficialTravelDetail");
                entity.HasKey(e => e.OfficialTravelDetailId);

                entity.Property(e => e.DepartureDate).HasColumnType("datetime");
                entity.Property(e => e.ReturnDate).HasColumnType("datetime");
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Seats).IsRequired();
                entity.Property(e => e.SoldSeats).IsRequired();
                entity.Property(e => e.MinimumGroupSize).IsRequired();
                entity.Property(e => e.BookingDeadline).HasColumnType("datetime");
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.GroupStatus)
                      .HasConversion<string>()
                      .HasMaxLength(20)
                      .IsRequired();

                entity.HasOne(d => d.OfficialTravel)
                      .WithMany(t => t.Details)
                      .HasForeignKey(d => d.OfficialTravelId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Flight)
                      .WithMany(f => f.OfficialTravelDetails)
                      .HasForeignKey(d => d.FlightId)
                      .OnDelete(DeleteBehavior.Restrict);

            });

            // T_OfficialTravelSchedule
            modelBuilder.Entity<OfficialTravelSchedule>(entity =>
            {
                entity.ToTable("T_OfficialTravelSchedule");
                entity.HasKey(s => s.OfficialTravelScheduleId);

                entity.Property(s => s.Day).IsRequired();
                entity.Property(s => s.StartTime).HasColumnType("time");
                entity.Property(s => s.Date).HasColumnType("date");
                entity.Property(s => s.Description).HasMaxLength(1000);
                entity.Property(s => s.Note1).HasMaxLength(500);
                entity.Property(s => s.Note2).HasMaxLength(500);

                entity.Property(s => s.Category)
                      .HasConversion<string>()
                      .HasMaxLength(30)
                      .IsRequired();

                entity.HasOne(s => s.OfficialTravelDetail)
                      .WithMany(d => d.Schedules)
                      .HasForeignKey(s => s.OfficialTravelDetailId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            // T_Region
            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("T_Region");
                entity.HasKey(r => r.RegionId);

                entity.Property(r => r.Country).IsRequired().HasMaxLength(100);
                entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
            });

            // T_Flight
            modelBuilder.Entity<Flight>(entity =>
            {
                entity.ToTable("T_Flight");
                entity.HasKey(e => e.FlightId);

                entity.Property(e => e.FlightUid).HasMaxLength(50);
                entity.HasIndex(e => e.FlightUid).IsUnique();

                entity.Property(e => e.AirlineCode).HasMaxLength(10);
                entity.Property(e => e.AirlineName).HasMaxLength(100);
                entity.Property(e => e.DepartureAirportCode).HasMaxLength(10);
                entity.Property(e => e.DepartureAirportName).HasMaxLength(100);
                entity.Property(e => e.ArrivalAirportCode).HasMaxLength(10);
                entity.Property(e => e.ArrivalAirportName).HasMaxLength(100);
                entity.Property(e => e.DepartureTime).HasColumnType("datetime");
                entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
                entity.Property(e => e.Status).HasMaxLength(20);
                entity.Property(e => e.AircraftType).HasMaxLength(50);
                entity.Property(e => e.SyncedAt).HasColumnType("datetime");
            });

            // T_TravelSupplier
            modelBuilder.Entity<TravelSupplier>(entity =>
            {
                entity.ToTable("T_TravelSupplier");
                entity.HasKey(e => e.TravelSupplierId);
                entity.Property(e => e.SupplierName).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.SupplierName).IsUnique();
                entity.Property(e => e.SupplierType).HasMaxLength(50);
                entity.Property(e => e.ContactName).HasMaxLength(50);
                entity.Property(e => e.ContactPhone).HasMaxLength(20);
                entity.Property(e => e.ContactEmail).HasMaxLength(100);
                entity.Property(e => e.Note).HasMaxLength(500);
            });

            // T_CustomTravel
            modelBuilder.Entity<CustomTravel>(entity =>
            {
                entity.ToTable("T_CustomTravel");
                entity.HasKey(e => e.CustomTravelId);
                entity.Property(e => e.MemberId).IsRequired();
                entity.Property(e => e.ReviewEmployeeId);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
                entity.Property(e => e.DepartureDate).HasColumnType("datetime");
                entity.Property(e => e.EndDate).HasColumnType("datetime");
                entity.Property(e => e.Days).IsRequired();
                entity.Property(e => e.People).IsRequired();
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Status)
                      .HasConversion<string>()
                      .HasMaxLength(20)
                      .IsRequired();

                entity.HasOne(e => e.Member)
                      .WithMany(o => o.CustomTravels)
                      .HasForeignKey(e => e.MemberId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ReviewEmployee)
                      .WithMany(o => o.ReviewedCustomTravels)
                      .HasForeignKey(e => e.ReviewEmployeeId)
                      .OnDelete(DeleteBehavior.Restrict);

            });

            // S_Transport
            modelBuilder.Entity<Transportation>(entity =>
            {
                entity.ToTable("S_Transport");
                entity.HasKey(e => e.TransportId);

                entity.Property(e => e.TransportMethod).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.TransportMethod).IsUnique();
            });

            // S_City
            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("S_City");
                entity.HasKey(e => e.CityId);

                entity.Property(e => e.CityName).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.CityName).IsUnique();
            });

            // S_District
            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("S_District");
                entity.HasKey(e => e.DistrictId);

                entity.Property(e => e.DistrictName).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => new { e.CityId, e.DistrictName }).IsUnique();

                entity.HasOne(d => d.City)
                      .WithMany(c => c.Districts)
                      .HasForeignKey(d => d.CityId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // T_Hotel
            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("T_Hotel");
                entity.HasKey(e => e.HotelId);

                entity.Property(e => e.HotelName).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => new { e.DistrictId, e.HotelName }).IsUnique();

                entity.HasOne(h => h.District)
                      .WithMany(o => o.Hotels)
                      .HasForeignKey(h => h.DistrictId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(h => h.TravelSupplier)
                      .WithMany(o => o.Hotels)
                      .HasForeignKey(h => h.TravelSupplierId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // T_Attraction
            modelBuilder.Entity<Attraction>(entity =>
            {
                entity.ToTable("T_Attraction");
                entity.HasKey(e => e.AttractionId);
                
                entity.Property(e => e.ScenicSpotName).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => new { e.DistrictId, e.ScenicSpotName }).IsUnique();

                entity.HasOne(h => h.District)
                      .WithMany(o => o.Attractions)
                      .HasForeignKey(h => h.DistrictId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(h => h.TravelSupplier)
                      .WithMany(o => o.Attractions)
                      .HasForeignKey(h => h.TravelSupplierId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // T_Restaurant
            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("T_Restaurant");
                entity.HasKey(e => e.RestaurantId);
                
                entity.Property(e => e.RestaurantName).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => new { e.DistrictId, e.RestaurantName }).IsUnique();

                entity.HasOne(h => h.District)
                      .WithMany(o => o.Restaurants)
                      .HasForeignKey(h => h.DistrictId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(h => h.TravelSupplier)
                      .WithMany(o => o.Restaurants)
                      .HasForeignKey(h => h.TravelSupplierId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // T_Content
            modelBuilder.Entity<Content>(entity =>
            {
                entity.ToTable("T_Content");
                entity.HasKey(e => e.ContentId);

                entity.Property(e => e.ItemId).IsRequired();
                entity.Property(e => e.Day).IsRequired();
                entity.Property(e => e.Time).HasColumnType("time");
                entity.Property(e => e.HotelName).HasMaxLength(100);

                entity.Property(e => e.Category)
                      .HasConversion<string>()
                      .HasMaxLength(30)
                      .IsRequired();

                entity.HasOne(c => c.CustomTravel)
                      .WithMany(t => t.Contents)
                      .HasForeignKey(c => c.CustomTravelId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // T_Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("T_Order");
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.MemberId).IsRequired();
                entity.Property(e => e.ItemId).IsRequired();

                entity.Property(e => e.ParticipantId).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");
                entity.Property(e => e.ParticipantsCount).IsRequired();
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.PaymentDate).HasColumnType("datetime");
                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.PaymentMethod)
                      .HasConversion<string>()
                      .HasMaxLength(20)
                      .IsRequired();
                entity.Property(e => e.Category)
                     .HasConversion<string>()
                     .HasMaxLength(20)
                     .IsRequired();
                entity.Property(e => e.Status)
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .IsRequired();

                entity.HasOne(e => e.Member)
                      .WithMany(o => o.Orders)
                      .HasForeignKey(e => e.MemberId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Participant)
                      .WithMany(o => o.Orders)
                      .HasForeignKey(e => e.ParticipantId)
                      .OnDelete(DeleteBehavior.Restrict);
                
            });

            // T_TravelRecord
            modelBuilder.Entity<TravelRecord>(entity =>
            {
                entity.ToTable("T_TravelRecord");
                entity.HasKey(t => t.TravelRecordId);

                entity.Property(t => t.TotalAmount).HasColumnType("decimal(18,2)");
                entity.Property(t => t.TotalParticipants).IsRequired();
                entity.Property(t => t.CreatedAt).HasColumnType("datetime");

                entity.HasOne(t => t.Order)
                      .WithMany(o => o.TravelRecords)
                      .HasForeignKey(t => t.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
