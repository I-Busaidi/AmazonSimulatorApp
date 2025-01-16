using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonSimulatorApp.Migrations
{
    /// <inheritdoc />
    public partial class addedImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductsImages",
                columns: table => new
                {
                    PID = table.Column<int>(type: "int", nullable: false),
                    ImgPath = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsImages", x => new { x.PID, x.ImgPath });
                    table.ForeignKey(
                        name: "FK_ProductsImages_Products_PID",
                        column: x => x.PID,
                        principalTable: "Products",
                        principalColumn: "PID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsImages");
        }
    }
}
