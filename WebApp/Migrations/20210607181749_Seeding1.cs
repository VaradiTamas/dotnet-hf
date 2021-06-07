using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class Seeding1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "DateAdded", "Description", "Genre", "NumOfRates", "ProducerId", "Rate", "Title" },
                values: new object[] { 1, new DateTime(2021, 6, 7, 20, 17, 49, 374, DateTimeKind.Local).AddTicks(6776), "1st Movie Description", "Comedy", 1, 2, 4.0, "1st Movie Title" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "DateAdded", "Description", "Genre", "NumOfRates", "ProducerId", "Rate", "Title" },
                values: new object[] { 2, new DateTime(2021, 6, 7, 20, 17, 49, 380, DateTimeKind.Local).AddTicks(6909), "2nd Movie Description", "Horror", null, 1, null, "2nd Movie Title" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
