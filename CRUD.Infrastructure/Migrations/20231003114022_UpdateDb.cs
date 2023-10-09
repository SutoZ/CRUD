using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CRUD.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Airplanes",
                keyColumn: "AirplaneId",
                keyValue: new Guid("ea82512c-e44e-4da1-832f-128f58884f4c"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: new Guid("3c616168-fe61-4215-81e9-9629357c9c16"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Countries",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50");

            migrationBuilder.InsertData(
                table: "Airplanes",
                columns: new[] { "AirplaneId", "Manufacturer", "Price", "Production", "Type" },
                values: new object[] { new Guid("69982775-c4b0-4da2-8cc7-b7533a6b67d0"), "Boeing", 15000000.0, new DateTime(2023, 10, 3, 13, 40, 21, 906, DateTimeKind.Local).AddTicks(8663), "737-700" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "CountryId", "Name", "Population", "Postal Code" },
                values: new object[] { new Guid("02b6597a-33f1-48e2-8d4e-3427e1d6134e"), null, "Budapest", 300000, "1058" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "Name" },
                values: new object[,]
                {
                    { new Guid("0919f2b8-96c4-4284-bf69-02881447abe1"), "Spain" },
                    { new Guid("ea5da711-9627-4191-bf9c-785eda9d7c6f"), "Germany" }
                });

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("fc8d48a1-a7fd-4dc4-88c0-fa512ff879ff"),
                column: "PersonalID",
                value: "123456");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Airplanes",
                keyColumn: "AirplaneId",
                keyValue: new Guid("69982775-c4b0-4da2-8cc7-b7533a6b67d0"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: new Guid("02b6597a-33f1-48e2-8d4e-3427e1d6134e"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: new Guid("0919f2b8-96c4-4284-bf69-02881447abe1"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: new Guid("ea5da711-9627-4191-bf9c-785eda9d7c6f"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Countries",
                type: "nvarchar(50",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.InsertData(
                table: "Airplanes",
                columns: new[] { "AirplaneId", "Manufacturer", "Price", "Production", "Type" },
                values: new object[] { new Guid("ea82512c-e44e-4da1-832f-128f58884f4c"), "Boeing", 15000000.0, new DateTime(2023, 9, 10, 15, 15, 57, 554, DateTimeKind.Local).AddTicks(4865), "737-700" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "CountryId", "Name", "Population", "Postal Code" },
                values: new object[] { new Guid("3c616168-fe61-4215-81e9-9629357c9c16"), null, "Budapest", 300000, "1058" });

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("fc8d48a1-a7fd-4dc4-88c0-fa512ff879ff"),
                column: "PersonalID",
                value: null);
        }
    }
}
