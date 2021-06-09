using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagementApp.Models.Database.Migrations
{
    public partial class MoreEntitiesFixedNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardUsers_board_BoardEntityId",
                table: "BoardUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckLists_items_ItemEntityId",
                table: "CheckLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Commentaries_items_ItemEntityId",
                table: "Commentaries");

            migrationBuilder.DropForeignKey(
                name: "FK_itemlist_board_BoardEntityId",
                table: "itemlist");

            migrationBuilder.DropForeignKey(
                name: "FK_items_itemlist_ItemListEntityId",
                table: "items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_items",
                table: "items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_itemlist",
                table: "itemlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_board",
                table: "board");

            migrationBuilder.RenameTable(
                name: "items",
                newName: "Items");

            migrationBuilder.RenameTable(
                name: "itemlist",
                newName: "ItemLists");

            migrationBuilder.RenameTable(
                name: "board",
                newName: "Boards");

            migrationBuilder.RenameIndex(
                name: "IX_items_ItemListEntityId",
                table: "Items",
                newName: "IX_Items_ItemListEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_itemlist_BoardEntityId",
                table: "ItemLists",
                newName: "IX_ItemLists_BoardEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemLists",
                table: "ItemLists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boards",
                table: "Boards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardUsers_Boards_BoardEntityId",
                table: "BoardUsers",
                column: "BoardEntityId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckLists_Items_ItemEntityId",
                table: "CheckLists",
                column: "ItemEntityId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaries_Items_ItemEntityId",
                table: "Commentaries",
                column: "ItemEntityId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemLists_Boards_BoardEntityId",
                table: "ItemLists",
                column: "BoardEntityId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemLists_ItemListEntityId",
                table: "Items",
                column: "ItemListEntityId",
                principalTable: "ItemLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardUsers_Boards_BoardEntityId",
                table: "BoardUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckLists_Items_ItemEntityId",
                table: "CheckLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Commentaries_Items_ItemEntityId",
                table: "Commentaries");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemLists_Boards_BoardEntityId",
                table: "ItemLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemLists_ItemListEntityId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemLists",
                table: "ItemLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boards",
                table: "Boards");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "items");

            migrationBuilder.RenameTable(
                name: "ItemLists",
                newName: "itemlist");

            migrationBuilder.RenameTable(
                name: "Boards",
                newName: "board");

            migrationBuilder.RenameIndex(
                name: "IX_Items_ItemListEntityId",
                table: "items",
                newName: "IX_items_ItemListEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemLists_BoardEntityId",
                table: "itemlist",
                newName: "IX_itemlist_BoardEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_items",
                table: "items",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_itemlist",
                table: "itemlist",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_board",
                table: "board",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardUsers_board_BoardEntityId",
                table: "BoardUsers",
                column: "BoardEntityId",
                principalTable: "board",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckLists_items_ItemEntityId",
                table: "CheckLists",
                column: "ItemEntityId",
                principalTable: "items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaries_items_ItemEntityId",
                table: "Commentaries",
                column: "ItemEntityId",
                principalTable: "items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
