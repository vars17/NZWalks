using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("45ebfa2f-371f-4879-9370-e5cf7effb71a"), "Diffcult" },
                    { new Guid("521a848c-bc36-4214-8bf9-eda6e28495c7"), "Easy" },
                    { new Guid("dfb11fe1-66c0-4fff-b99f-d30ffa72bd16"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("02fa0853-7bca-4d19-9f53-dc0a5e9ddc91"), "AKL", "Auckland", "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("421cc33d-34b9-49c6-a76b-6f4bfb1344ff"), "NTL", "Northland", null },
                    { new Guid("4848ae00-78e7-4418-93fe-58bc87944056"), "WGN", "Wellington", "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("ca085225-39e9-4c98-87ba-8fc1ed57c791"), "BOP", "Bay Of Plenty", null },
                    { new Guid("d5d77a79-0af8-43f1-a59c-66183b51b3a9"), "STL", "Southland", null },
                    { new Guid("d8dbfada-d782-41bf-96fb-6690145ba2b5"), "NSN", "Nelson", "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("45ebfa2f-371f-4879-9370-e5cf7effb71a"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("521a848c-bc36-4214-8bf9-eda6e28495c7"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("dfb11fe1-66c0-4fff-b99f-d30ffa72bd16"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("02fa0853-7bca-4d19-9f53-dc0a5e9ddc91"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("421cc33d-34b9-49c6-a76b-6f4bfb1344ff"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("4848ae00-78e7-4418-93fe-58bc87944056"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ca085225-39e9-4c98-87ba-8fc1ed57c791"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d5d77a79-0af8-43f1-a59c-66183b51b3a9"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d8dbfada-d782-41bf-96fb-6690145ba2b5"));

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Regions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
