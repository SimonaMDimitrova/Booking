using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking.Data.Migrations
{
    public partial class SetBaseModelToPropertyFacilitiesEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PropertyFacilities_IsDeleted",
                table: "PropertyFacilities");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PropertyFacilities");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PropertyFacilities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PropertyFacilities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PropertyFacilities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFacilities_IsDeleted",
                table: "PropertyFacilities",
                column: "IsDeleted");
        }
    }
}
