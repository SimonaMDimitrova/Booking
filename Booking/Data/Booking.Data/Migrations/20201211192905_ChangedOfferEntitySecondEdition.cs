using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking.Data.Migrations
{
    public partial class ChangedOfferEntitySecondEdition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancellationDays",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "IsCreditCardAllowed",
                table: "Offers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "CancellationDays",
                table: "Offers",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCreditCardAllowed",
                table: "Offers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
