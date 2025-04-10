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
            //migrationBuilder.AddColumn<string>(
            //    name: "Caption",
            //    table: "T_Permission",
            //    type: "nvarchar(100)",
            //    maxLength: 100,
            //    nullable: true);

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "PassportIssueDate",
            //    table: "T_Participant",
            //    type: "date",
            //    nullable: true,
            //    oldClrType: typeof(DateTime),
            //    oldType: "date");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Gender",
            //    table: "T_Participant",
            //    type: "nvarchar(20)",
            //    maxLength: 20,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(10)",
            //    oldMaxLength: 10);

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "BirthDate",
            //    table: "T_Participant",
            //    type: "date",
            //    nullable: true,
            //    oldClrType: typeof(DateTime),
            //    oldType: "date");

            //migrationBuilder.AddColumn<string>(
            //    name: "Address",
            //    table: "T_Employee",
            //    type: "nvarchar(255)",
            //    maxLength: 255,
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<int>(
            //    name: "Gender",
            //    table: "T_Employee",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caption",
                table: "T_Permission");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "T_Employee");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "T_Employee");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PassportIssueDate",
                table: "T_Participant",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "T_Participant",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "T_Participant",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);
        }
    }
}
