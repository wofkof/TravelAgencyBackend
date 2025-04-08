using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddCaption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "T_Permission",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caption",
                table: "T_Permission");
        }
    }
}
