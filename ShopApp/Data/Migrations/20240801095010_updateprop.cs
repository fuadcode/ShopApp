using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentPrice",
                table: "Products",
                newName: "CostPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CostPrice",
                table: "Products",
                newName: "CurrentPrice");
        }
    }
}
