using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyBackend.Migrations
{
    /// <inheritdoc />
    public partial class Updatenull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
               name: "UpdatedAt",
               table: "T_OfficialTravelDetail",
               type: "datetime2",
               nullable: true,
               oldClrType: typeof(DateTime),
               oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "T_OfficialTravelDetail",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
