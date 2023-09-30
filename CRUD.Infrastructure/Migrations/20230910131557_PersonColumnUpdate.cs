using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PersonColumnUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "PostalCode",
                table: "Cities",
                newName: "Postal Code");

            migrationBuilder.AddColumn<string>(
                name: "PersonalID",
                table: "Persons",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Countries",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Postal Code",
                table: "Cities",
                type: "varchar(8)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "Cities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Airplanes",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Manufacturer",
                table: "Airplanes",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CountryId",
                table: "Persons",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PersonalID",
                table: "Persons",
                column: "PersonalID",
                unique: true,
                filter: "[PersonalID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Countries_CountryId",
                table: "Persons",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Countries_CountryId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_CountryId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_PersonalID",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CountryId",
                table: "Cities");

            migrationBuilder.DeleteData(
                table: "Airplanes",
                keyColumn: "AirplaneId",
                keyValue: new Guid("ea82512c-e44e-4da1-832f-128f58884f4c"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: new Guid("3c616168-fe61-4215-81e9-9629357c9c16"));

            migrationBuilder.DropColumn(
                name: "PersonalID",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "Postal Code",
                table: "Cities",
                newName: "PostalCode");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Airplanes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Manufacturer",
                table: "Airplanes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Airplanes",
                columns: new[] { "AirplaneId", "Manufacturer", "Price", "Production", "Type" },
                values: new object[] { new Guid("9d621276-b4fd-41ad-a995-6a8f9a60cb2a"), "Boeing", 15000000.0, new DateTime(2023, 9, 7, 23, 16, 11, 662, DateTimeKind.Local).AddTicks(3912), "737-700" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "Name", "Population", "PostalCode" },
                values: new object[] { new Guid("6cf29c0a-eef3-4160-ae95-4d11bf514fd3"), "Budapest", 300000, "1058" });
        }
    }
}
