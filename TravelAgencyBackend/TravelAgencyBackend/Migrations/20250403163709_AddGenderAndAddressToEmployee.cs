using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderAndAddressToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "T_Employee",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "T_Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "T_Employee");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "T_Employee");
        }
    }
}
