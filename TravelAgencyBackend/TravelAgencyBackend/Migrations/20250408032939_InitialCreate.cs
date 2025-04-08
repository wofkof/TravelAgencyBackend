using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "S_City",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_S_City", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "S_Transport",
                columns: table => new
                {
                    TransportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransportMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_S_Transport", x => x.TransportId);
                });

            migrationBuilder.CreateTable(
                name: "T_Flight",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirlineCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AirlineName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DepartureAirportCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DepartureAirportName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArrivalAirportCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ArrivalAirportName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AircraftType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FlightUid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SyncedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Flight", x => x.FlightId);
                });

            migrationBuilder.CreateTable(
                name: "T_Member",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Member", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "T_Permission",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Permission", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "T_Region",
                columns: table => new
                {
                    RegionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Region", x => x.RegionId);
                });

            migrationBuilder.CreateTable(
                name: "T_Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "T_TravelSupplier",
                columns: table => new
                {
                    TravelSupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SupplierType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_TravelSupplier", x => x.TravelSupplierId);
                });

            migrationBuilder.CreateTable(
                name: "S_District",
                columns: table => new
                {
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    DistrictName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_S_District", x => x.DistrictId);
                    table.ForeignKey(
                        name: "FK_S_District_S_City_CityId",
                        column: x => x.CityId,
                        principalTable: "S_City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_Cart",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Cart", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_T_Cart_T_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "T_Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_Participant",
                columns: table => new
                {
                    ParticipantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IdNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IssuedPlace = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PassportIssueDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Participant", x => x.ParticipantId);
                    table.ForeignKey(
                        name: "FK_T_Participant_T_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "T_Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "M_RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_RolePermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_M_RolePermission_T_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "T_Permission",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_M_RolePermission_T_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "T_Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    HireDate = table.Column<DateTime>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_T_Employee_T_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "T_Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_Attraction",
                columns: table => new
                {
                    AttractionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    TravelSupplierId = table.Column<int>(type: "int", nullable: false),
                    ScenicSpotName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Attraction", x => x.AttractionId);
                    table.ForeignKey(
                        name: "FK_T_Attraction_S_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "S_District",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_Attraction_T_TravelSupplier_TravelSupplierId",
                        column: x => x.TravelSupplierId,
                        principalTable: "T_TravelSupplier",
                        principalColumn: "TravelSupplierId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_Hotel",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    TravelSupplierId = table.Column<int>(type: "int", nullable: false),
                    HotelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Hotel", x => x.HotelId);
                    table.ForeignKey(
                        name: "FK_T_Hotel_S_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "S_District",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_Hotel_T_TravelSupplier_TravelSupplierId",
                        column: x => x.TravelSupplierId,
                        principalTable: "T_TravelSupplier",
                        principalColumn: "TravelSupplierId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_Restaurant",
                columns: table => new
                {
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    TravelSupplierId = table.Column<int>(type: "int", nullable: false),
                    RestaurantName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Restaurant", x => x.RestaurantId);
                    table.ForeignKey(
                        name: "FK_T_Restaurant_S_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "S_District",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_Restaurant_T_TravelSupplier_TravelSupplierId",
                        column: x => x.TravelSupplierId,
                        principalTable: "T_TravelSupplier",
                        principalColumn: "TravelSupplierId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_Announcement",
                columns: table => new
                {
                    AnnouncementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Announcement", x => x.AnnouncementId);
                    table.ForeignKey(
                        name: "FK_T_Announcement_T_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "T_Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_ChatRoom",
                columns: table => new
                {
                    ChatRoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_ChatRoom", x => x.ChatRoomId);
                    table.ForeignKey(
                        name: "FK_T_ChatRoom_T_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "T_Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_ChatRoom_T_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "T_Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_CustomTravel",
                columns: table => new
                {
                    CustomTravelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    ReviewEmployeeId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    People = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CustomTravel", x => x.CustomTravelId);
                    table.ForeignKey(
                        name: "FK_T_CustomTravel_T_Employee_ReviewEmployeeId",
                        column: x => x.ReviewEmployeeId,
                        principalTable: "T_Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_CustomTravel_T_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "T_Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_OfficialTravel",
                columns: table => new
                {
                    OfficialTravelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByEmployeeId = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProjectYear = table.Column<int>(type: "int", nullable: false),
                    AvailableFrom = table.Column<DateTime>(type: "date", nullable: false),
                    AvailableUntil = table.Column<DateTime>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    CoverPath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_OfficialTravel", x => x.OfficialTravelId);
                    table.ForeignKey(
                        name: "FK_T_OfficialTravel_T_Employee_CreatedByEmployeeId",
                        column: x => x.CreatedByEmployeeId,
                        principalTable: "T_Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_OfficialTravel_T_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "T_Region",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_Message",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatRoomId = table.Column<int>(type: "int", nullable: false),
                    SenderType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Message", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_T_Message_T_ChatRoom_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalTable: "T_ChatRoom",
                        principalColumn: "ChatRoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Content",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomTravelId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    HotelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Content", x => x.ContentId);
                    table.ForeignKey(
                        name: "FK_T_Content_T_CustomTravel_CustomTravelId",
                        column: x => x.CustomTravelId,
                        principalTable: "T_CustomTravel",
                        principalColumn: "CustomTravelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_OfficialTravelDetail",
                columns: table => new
                {
                    OfficialTravelDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficialTravelId = table.Column<int>(type: "int", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    SoldSeats = table.Column<int>(type: "int", nullable: false),
                    MinimumGroupSize = table.Column<int>(type: "int", nullable: false),
                    BookingDeadline = table.Column<DateTime>(type: "datetime", nullable: false),
                    GroupStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    TravelSupplierId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_OfficialTravelDetail", x => x.OfficialTravelDetailId);
                    table.ForeignKey(
                        name: "FK_T_OfficialTravelDetail_T_Flight_FlightId",
                        column: x => x.FlightId,
                        principalTable: "T_Flight",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_OfficialTravelDetail_T_OfficialTravel_OfficialTravelId",
                        column: x => x.OfficialTravelId,
                        principalTable: "T_OfficialTravel",
                        principalColumn: "OfficialTravelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_OfficialTravelDetail_T_TravelSupplier_TravelSupplierId",
                        column: x => x.TravelSupplierId,
                        principalTable: "T_TravelSupplier",
                        principalColumn: "TravelSupplierId");
                });

            migrationBuilder.CreateTable(
                name: "T_OfficialTravelSchedule",
                columns: table => new
                {
                    OfficialTravelScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficialTravelDetailId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Note1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Note2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_OfficialTravelSchedule", x => x.OfficialTravelScheduleId);
                    table.ForeignKey(
                        name: "FK_T_OfficialTravelSchedule_T_OfficialTravelDetail_OfficialTravelDetailId",
                        column: x => x.OfficialTravelDetailId,
                        principalTable: "T_OfficialTravelDetail",
                        principalColumn: "OfficialTravelDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ParticipantId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ParticipantsCount = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CustomTravelId = table.Column<int>(type: "int", nullable: true),
                    OfficialTravelDetailId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_T_Order_T_CustomTravel_CustomTravelId",
                        column: x => x.CustomTravelId,
                        principalTable: "T_CustomTravel",
                        principalColumn: "CustomTravelId");
                    table.ForeignKey(
                        name: "FK_T_Order_T_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "T_Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_Order_T_OfficialTravelDetail_OfficialTravelDetailId",
                        column: x => x.OfficialTravelDetailId,
                        principalTable: "T_OfficialTravelDetail",
                        principalColumn: "OfficialTravelDetailId");
                    table.ForeignKey(
                        name: "FK_T_Order_T_Participant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "T_Participant",
                        principalColumn: "ParticipantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_TravelRecord",
                columns: table => new
                {
                    TravelRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalParticipants = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_TravelRecord", x => x.TravelRecordId);
                    table.ForeignKey(
                        name: "FK_T_TravelRecord_T_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "T_Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_M_RolePermission_PermissionId",
                table: "M_RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_S_City_CityName",
                table: "S_City",
                column: "CityName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_S_District_CityId_DistrictName",
                table: "S_District",
                columns: new[] { "CityId", "DistrictName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_S_Transport_TransportMethod",
                table: "S_Transport",
                column: "TransportMethod",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Announcement_EmployeeId",
                table: "T_Announcement",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Attraction_DistrictId_ScenicSpotName",
                table: "T_Attraction",
                columns: new[] { "DistrictId", "ScenicSpotName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Attraction_TravelSupplierId",
                table: "T_Attraction",
                column: "TravelSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Cart_MemberId",
                table: "T_Cart",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_T_ChatRoom_EmployeeId",
                table: "T_ChatRoom",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_T_ChatRoom_MemberId",
                table: "T_ChatRoom",
                column: "MemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Content_CustomTravelId",
                table: "T_Content",
                column: "CustomTravelId");

            migrationBuilder.CreateIndex(
                name: "IX_T_CustomTravel_MemberId",
                table: "T_CustomTravel",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_T_CustomTravel_ReviewEmployeeId",
                table: "T_CustomTravel",
                column: "ReviewEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Employee_Email",
                table: "T_Employee",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Employee_Phone",
                table: "T_Employee",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Employee_RoleId",
                table: "T_Employee",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Flight_FlightUid",
                table: "T_Flight",
                column: "FlightUid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Hotel_DistrictId_HotelName",
                table: "T_Hotel",
                columns: new[] { "DistrictId", "HotelName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Hotel_TravelSupplierId",
                table: "T_Hotel",
                column: "TravelSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Member_Account",
                table: "T_Member",
                column: "Account",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Member_Email",
                table: "T_Member",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Member_Phone",
                table: "T_Member",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Message_ChatRoomId",
                table: "T_Message",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_T_OfficialTravel_CreatedByEmployeeId",
                table: "T_OfficialTravel",
                column: "CreatedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_T_OfficialTravel_RegionId",
                table: "T_OfficialTravel",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_T_OfficialTravelDetail_FlightId",
                table: "T_OfficialTravelDetail",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_T_OfficialTravelDetail_OfficialTravelId",
                table: "T_OfficialTravelDetail",
                column: "OfficialTravelId");

            migrationBuilder.CreateIndex(
                name: "IX_T_OfficialTravelDetail_TravelSupplierId",
                table: "T_OfficialTravelDetail",
                column: "TravelSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_T_OfficialTravelSchedule_OfficialTravelDetailId",
                table: "T_OfficialTravelSchedule",
                column: "OfficialTravelDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Order_CustomTravelId",
                table: "T_Order",
                column: "CustomTravelId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Order_MemberId",
                table: "T_Order",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Order_OfficialTravelDetailId",
                table: "T_Order",
                column: "OfficialTravelDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Order_ParticipantId",
                table: "T_Order",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Participant_IdNumber",
                table: "T_Participant",
                column: "IdNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Participant_MemberId",
                table: "T_Participant",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Participant_PassportNumber",
                table: "T_Participant",
                column: "PassportNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Participant_Phone",
                table: "T_Participant",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Restaurant_DistrictId_RestaurantName",
                table: "T_Restaurant",
                columns: new[] { "DistrictId", "RestaurantName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Restaurant_TravelSupplierId",
                table: "T_Restaurant",
                column: "TravelSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_T_TravelRecord_OrderId",
                table: "T_TravelRecord",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_T_TravelSupplier_SupplierName",
                table: "T_TravelSupplier",
                column: "SupplierName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "M_RolePermission");

            migrationBuilder.DropTable(
                name: "S_Transport");

            migrationBuilder.DropTable(
                name: "T_Announcement");

            migrationBuilder.DropTable(
                name: "T_Attraction");

            migrationBuilder.DropTable(
                name: "T_Cart");

            migrationBuilder.DropTable(
                name: "T_Content");

            migrationBuilder.DropTable(
                name: "T_Hotel");

            migrationBuilder.DropTable(
                name: "T_Message");

            migrationBuilder.DropTable(
                name: "T_OfficialTravelSchedule");

            migrationBuilder.DropTable(
                name: "T_Restaurant");

            migrationBuilder.DropTable(
                name: "T_TravelRecord");

            migrationBuilder.DropTable(
                name: "T_Permission");

            migrationBuilder.DropTable(
                name: "T_ChatRoom");

            migrationBuilder.DropTable(
                name: "S_District");

            migrationBuilder.DropTable(
                name: "T_Order");

            migrationBuilder.DropTable(
                name: "S_City");

            migrationBuilder.DropTable(
                name: "T_CustomTravel");

            migrationBuilder.DropTable(
                name: "T_OfficialTravelDetail");

            migrationBuilder.DropTable(
                name: "T_Participant");

            migrationBuilder.DropTable(
                name: "T_Flight");

            migrationBuilder.DropTable(
                name: "T_OfficialTravel");

            migrationBuilder.DropTable(
                name: "T_TravelSupplier");

            migrationBuilder.DropTable(
                name: "T_Member");

            migrationBuilder.DropTable(
                name: "T_Employee");

            migrationBuilder.DropTable(
                name: "T_Region");

            migrationBuilder.DropTable(
                name: "T_Role");
        }
    }
}
