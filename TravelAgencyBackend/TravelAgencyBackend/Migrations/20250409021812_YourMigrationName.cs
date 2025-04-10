using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyBackend.Migrations
{
    /// <inheritdoc />
    public partial class YourMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_T_OfficialTravelDetail_T_TravelSupplier_TravelSupplierId",
            //    table: "T_OfficialTravelDetail");

            //migrationBuilder.DropIndex(
            //    name: "IX_T_OfficialTravelDetail_TravelSupplierId",
            //    table: "T_OfficialTravelDetail");

            //migrationBuilder.DropColumn(
            //    name: "TravelSupplierId",
            //    table: "T_OfficialTravelDetail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TravelSupplierId",
                table: "T_OfficialTravelDetail",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_OfficialTravelDetail_TravelSupplierId",
                table: "T_OfficialTravelDetail",
                column: "TravelSupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_T_OfficialTravelDetail_T_TravelSupplier_TravelSupplierId",
                table: "T_OfficialTravelDetail",
                column: "TravelSupplierId",
                principalTable: "T_TravelSupplier",
                principalColumn: "TravelSupplierId");
        }
    }
}
