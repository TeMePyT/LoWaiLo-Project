namespace LoWaiLo.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Cleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavoriteProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFavoriteProducts",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteProducts", x => new { x.ProductId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserFavoriteProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFavoriteProducts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteProducts_UserId",
                table: "UserFavoriteProducts",
                column: "UserId");
        }
    }
}
