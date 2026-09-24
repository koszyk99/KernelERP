using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KernelErp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCreatedAtTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "StockItems",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Products",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "StockItems",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Products",
                newName: "CreateAt");
        }
    }
}
