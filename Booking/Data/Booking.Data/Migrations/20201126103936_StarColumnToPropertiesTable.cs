using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking.Data.Migrations
{
    public partial class StarColumnToPropertiesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Stars",
                table: "Properties",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stars",
                table: "Properties");
        }
    }
}
