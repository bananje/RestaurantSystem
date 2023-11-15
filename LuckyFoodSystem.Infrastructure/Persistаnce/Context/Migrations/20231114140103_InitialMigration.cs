using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckyFoodSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageMenu_Images_ImagesId",
                table: "ImageMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageMenu_Menus_MenusId",
                table: "ImageMenu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageMenu",
                table: "ImageMenu");

            migrationBuilder.RenameTable(
                name: "ImageMenu",
                newName: "MenuImage");

            migrationBuilder.RenameIndex(
                name: "IX_ImageMenu_MenusId",
                table: "MenuImage",
                newName: "IX_MenuImage_MenusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuImage",
                table: "MenuImage",
                columns: new[] { "ImagesId", "MenusId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MenuImage_Images_ImagesId",
                table: "MenuImage",
                column: "ImagesId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuImage_Menus_MenusId",
                table: "MenuImage",
                column: "MenusId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuImage_Images_ImagesId",
                table: "MenuImage");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuImage_Menus_MenusId",
                table: "MenuImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuImage",
                table: "MenuImage");

            migrationBuilder.RenameTable(
                name: "MenuImage",
                newName: "ImageMenu");

            migrationBuilder.RenameIndex(
                name: "IX_MenuImage_MenusId",
                table: "ImageMenu",
                newName: "IX_ImageMenu_MenusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageMenu",
                table: "ImageMenu",
                columns: new[] { "ImagesId", "MenusId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ImageMenu_Images_ImagesId",
                table: "ImageMenu",
                column: "ImagesId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageMenu_Menus_MenusId",
                table: "ImageMenu",
                column: "MenusId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
