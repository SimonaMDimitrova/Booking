using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking.Data.Migrations
{
    public partial class ChangeEntitiesInhiritance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Towns_IsDeleted",
                table: "Towns");

            migrationBuilder.DropIndex(
                name: "IX_Rules_IsDeleted",
                table: "Rules");

            migrationBuilder.DropIndex(
                name: "IX_PropertyTypes_IsDeleted",
                table: "PropertyTypes");

            migrationBuilder.DropIndex(
                name: "IX_PropertyCategories_IsDeleted",
                table: "PropertyCategories");

            migrationBuilder.DropIndex(
                name: "IX_FacilityCategories_IsDeleted",
                table: "FacilityCategories");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_IsDeleted",
                table: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_IsDeleted",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Countries_IsDeleted",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_BedTypes_IsDeleted",
                table: "BedTypes");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PropertyTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PropertyTypes");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PropertyCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PropertyCategories");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "FacilityCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FacilityCategories");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "BedTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BedTypes");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Offers",
                newName: "ValidTo");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Offers",
                newName: "ValidFrom");

            migrationBuilder.AlterColumn<string>(
                name: "OfferId",
                table: "Rooms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValidTo",
                table: "Offers",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "ValidFrom",
                table: "Offers",
                newName: "EndDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Towns",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Towns",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Rules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Rules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "OfferId",
                table: "Rooms",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PropertyTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PropertyTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PropertyCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PropertyCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "FacilityCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FacilityCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Facilities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Facilities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Currencies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Currencies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Countries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Countries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "BedTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BedTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Towns_IsDeleted",
                table: "Towns",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_IsDeleted",
                table: "Rules",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyTypes_IsDeleted",
                table: "PropertyTypes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyCategories_IsDeleted",
                table: "PropertyCategories",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityCategories_IsDeleted",
                table: "FacilityCategories",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_IsDeleted",
                table: "Facilities",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_IsDeleted",
                table: "Currencies",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_IsDeleted",
                table: "Countries",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_BedTypes_IsDeleted",
                table: "BedTypes",
                column: "IsDeleted");
        }
    }
}
