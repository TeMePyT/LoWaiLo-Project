namespace LoWaiLo.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedAdressToUserAndORders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryAdress",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryAdress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Adress",
                table: "AspNetUsers");
        }
    }
}
