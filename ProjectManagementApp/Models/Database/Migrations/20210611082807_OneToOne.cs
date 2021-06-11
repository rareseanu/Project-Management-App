using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagementApp.Models.Database.Migrations
{
    public partial class OneToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemLabels_Items_ItemEntityId",
                table: "ItemLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemLabels_Labels_LabelEntityId",
                table: "ItemLabels");

            migrationBuilder.DropIndex(
                name: "IX_CheckLists_ItemEntityId",
                table: "CheckLists");

            migrationBuilder.AlterColumn<int>(
                name: "LabelEntityId",
                table: "ItemLabels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ItemEntityId",
                table: "ItemLabels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckLists_ItemEntityId",
                table: "CheckLists",
                column: "ItemEntityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemLabels_Items_ItemEntityId",
                table: "ItemLabels",
                column: "ItemEntityId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemLabels_Labels_LabelEntityId",
                table: "ItemLabels",
                column: "LabelEntityId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemLabels_Items_ItemEntityId",
                table: "ItemLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemLabels_Labels_LabelEntityId",
                table: "ItemLabels");

            migrationBuilder.DropIndex(
                name: "IX_CheckLists_ItemEntityId",
                table: "CheckLists");

            migrationBuilder.AlterColumn<int>(
                name: "LabelEntityId",
                table: "ItemLabels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemEntityId",
                table: "ItemLabels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_CheckLists_ItemEntityId",
                table: "CheckLists",
                column: "ItemEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemLabels_Items_ItemEntityId",
                table: "ItemLabels",
                column: "ItemEntityId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemLabels_Labels_LabelEntityId",
                table: "ItemLabels",
                column: "LabelEntityId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
