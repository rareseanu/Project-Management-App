using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagementApp.Models.Database.Migrations
{
    public partial class MoreEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itemlist_board_BoardEntityId",
                table: "itemlist");

            migrationBuilder.DropForeignKey(
                name: "FK_items_itemlist_ItemListEntityId",
                table: "items");

            migrationBuilder.AlterColumn<int>(
                name: "ItemListEntityId",
                table: "items",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BoardEntityId",
                table: "itemlist",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "BoardUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEntityId = table.Column<int>(type: "int", nullable: false),
                    BoardEntityId = table.Column<int>(type: "int", nullable: false),
                    BoardRole = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardUsers_AspNetUsers_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardUsers_board_BoardEntityId",
                        column: x => x.BoardEntityId,
                        principalTable: "board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemEntityId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckLists_items_ItemEntityId",
                        column: x => x.ItemEntityId,
                        principalTable: "items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commentaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemEntityId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commentaries_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commentaries_items_ItemEntityId",
                        column: x => x.ItemEntityId,
                        principalTable: "items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Task = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsChecked = table.Column<bool>(type: "bit", nullable: false),
                    CheckListEntityId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckItems_CheckLists_CheckListEntityId",
                        column: x => x.CheckListEntityId,
                        principalTable: "CheckLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardUsers_BoardEntityId",
                table: "BoardUsers",
                column: "BoardEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardUsers_UserEntityId",
                table: "BoardUsers",
                column: "UserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckItems_CheckListEntityId",
                table: "CheckItems",
                column: "CheckListEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckLists_ItemEntityId",
                table: "CheckLists",
                column: "ItemEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Commentaries_CreatedBy",
                table: "Commentaries",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Commentaries_ItemEntityId",
                table: "Commentaries",
                column: "ItemEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_itemlist_board_BoardEntityId",
                table: "itemlist",
                column: "BoardEntityId",
                principalTable: "board",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_items_itemlist_ItemListEntityId",
                table: "items",
                column: "ItemListEntityId",
                principalTable: "itemlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itemlist_board_BoardEntityId",
                table: "itemlist");

            migrationBuilder.DropForeignKey(
                name: "FK_items_itemlist_ItemListEntityId",
                table: "items");

            migrationBuilder.DropTable(
                name: "BoardUsers");

            migrationBuilder.DropTable(
                name: "CheckItems");

            migrationBuilder.DropTable(
                name: "Commentaries");

            migrationBuilder.DropTable(
                name: "CheckLists");

            migrationBuilder.AlterColumn<int>(
                name: "ItemListEntityId",
                table: "items",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BoardEntityId",
                table: "itemlist",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_itemlist_board_BoardEntityId",
                table: "itemlist",
                column: "BoardEntityId",
                principalTable: "board",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_items_itemlist_ItemListEntityId",
                table: "items",
                column: "ItemListEntityId",
                principalTable: "itemlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
