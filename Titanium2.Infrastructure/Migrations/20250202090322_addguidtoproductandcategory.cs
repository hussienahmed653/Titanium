using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Titanium2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addguidtoproductandcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductGuid",
                table: "Product",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryGuid",
                table: "Category",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductGuid",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CategoryGuid",
                table: "Category");
        }
    }
}
