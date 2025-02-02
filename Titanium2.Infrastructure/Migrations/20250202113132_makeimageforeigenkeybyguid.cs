using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Titanium2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class makeimageforeigenkeybyguid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Product_ProductId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductImages");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductGuid",
                table: "ProductImages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Product_ProductGuid",
                table: "Product",
                column: "ProductGuid");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductGuid",
                table: "ProductImages",
                column: "ProductGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Product_ProductGuid",
                table: "ProductImages",
                column: "ProductGuid",
                principalTable: "Product",
                principalColumn: "ProductGuid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Product_ProductGuid",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductGuid",
                table: "ProductImages");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Product_ProductGuid",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductGuid",
                table: "ProductImages");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductImages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Product_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
