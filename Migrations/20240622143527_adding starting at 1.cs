using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_MVC.Migrations
{
    /// <inheritdoc />
    public partial class addingstartingat1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "CartItems",
                type: "int",
                nullable: true,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
