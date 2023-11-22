using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckyFoodSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ImplementWeight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_WeightUnits_Weight_WeightUnitId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "WeightUnits");

            migrationBuilder.DropIndex(
                name: "IX_Products_Weight_WeightUnitId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Weight_WeightUnitId",
                table: "Products",
                newName: "Weight_WeightUnit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight_WeightUnit",
                table: "Products",
                newName: "Weight_WeightUnitId");

            
        }
    }
}
