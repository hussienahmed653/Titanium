using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Titanium2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeproductimagemodelfromdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Product_ProductGuid",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "integer", nullable: false),
                    FileGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    FolderGuid = table.Column<Guid>(type: "uuid", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    Extention = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Product_ProductGuid",
                table: "Product",
                column: "ProductGuid");

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ProductImageId = table.Column<int>(type: "integer", nullable: false),
                    ProductGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: false),
                    ProductImageGuid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.ProductImageId);
                    table.ForeignKey(
                        name: "FK_ProductImages_Product_ProductGuid",
                        column: x => x.ProductGuid,
                        principalTable: "Product",
                        principalColumn: "ProductGuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductGuid",
                table: "ProductImages",
                column: "ProductGuid");
        }
    }
}
