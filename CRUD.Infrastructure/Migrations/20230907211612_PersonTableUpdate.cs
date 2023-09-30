using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PersonTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Airplanes",
                keyColumn: "AirplaneId",
                keyValue: new Guid("09c7156b-412e-475e-9ec3-a936e7da7413"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: new Guid("61009b14-7417-4a0c-a258-834fe47cd739"));

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Persons",
                newName: "Date of birth");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Persons",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "Persons",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Persons",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Airplanes",
                columns: new[] { "AirplaneId", "Manufacturer", "Price", "Production", "Type" },
                values: new object[] { new Guid("9d621276-b4fd-41ad-a995-6a8f9a60cb2a"), "Boeing", 15000000.0, new DateTime(2023, 9, 7, 23, 16, 11, 662, DateTimeKind.Local).AddTicks(3912), "737-700" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "Name", "Population", "PostalCode" },
                values: new object[] { new Guid("6cf29c0a-eef3-4160-ae95-4d11bf514fd3"), "Budapest", 300000, "1058" });

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("fc8d48a1-a7fd-4dc4-88c0-fa512ff879ff"),
                column: "Gender",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Airplanes",
                keyColumn: "AirplaneId",
                keyValue: new Guid("9d621276-b4fd-41ad-a995-6a8f9a60cb2a"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: new Guid("6cf29c0a-eef3-4160-ae95-4d11bf514fd3"));

            migrationBuilder.RenameColumn(
                name: "Date of birth",
                table: "Persons",
                newName: "DateOfBirth");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Airplanes",
                columns: new[] { "AirplaneId", "Manufacturer", "Price", "Production", "Type" },
                values: new object[] { new Guid("09c7156b-412e-475e-9ec3-a936e7da7413"), "Boeing", 15000000.0, new DateTime(2023, 9, 4, 23, 37, 25, 907, DateTimeKind.Local).AddTicks(4934), "737-700" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "Name", "Population", "PostalCode" },
                values: new object[] { new Guid("61009b14-7417-4a0c-a258-834fe47cd739"), "Budapest", 300000, "1058" });

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonId",
                keyValue: new Guid("fc8d48a1-a7fd-4dc4-88c0-fa512ff879ff"),
                column: "Gender",
                value: null);
        }
    }
}
