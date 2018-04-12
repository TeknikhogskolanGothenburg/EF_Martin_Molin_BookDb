using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookManager.Data.Migrations
{
    public partial class addedMissingDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCollection_Books_BookId",
                table: "BookCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCollection_Collection_CollectionId",
                table: "BookCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_Collection_User_UserId",
                table: "Collection");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Books_BookId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_User_UserId",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rating",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collection",
                table: "Collection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookCollection",
                table: "BookCollection");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Rating",
                newName: "Ratings");

            migrationBuilder.RenameTable(
                name: "Collection",
                newName: "Collections");

            migrationBuilder.RenameTable(
                name: "BookCollection",
                newName: "BookCollections");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_BookId",
                table: "Ratings",
                newName: "IX_Ratings_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Collection_UserId",
                table: "Collections",
                newName: "IX_Collections_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookCollection_CollectionId",
                table: "BookCollections",
                newName: "IX_BookCollections_CollectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                columns: new[] { "UserId", "BookId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookCollections",
                table: "BookCollections",
                columns: new[] { "BookId", "CollectionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookCollections_Books_BookId",
                table: "BookCollections",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCollections_Collections_CollectionId",
                table: "BookCollections",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Users_UserId",
                table: "Collections",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Books_BookId",
                table: "Ratings",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_UserId",
                table: "Ratings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCollections_Books_BookId",
                table: "BookCollections");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCollections_Collections_CollectionId",
                table: "BookCollections");

            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Users_UserId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Books_BookId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_UserId",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookCollections",
                table: "BookCollections");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "Rating");

            migrationBuilder.RenameTable(
                name: "Collections",
                newName: "Collection");

            migrationBuilder.RenameTable(
                name: "BookCollections",
                newName: "BookCollection");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_BookId",
                table: "Rating",
                newName: "IX_Rating_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Collections_UserId",
                table: "Collection",
                newName: "IX_Collection_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookCollections_CollectionId",
                table: "BookCollection",
                newName: "IX_BookCollection_CollectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rating",
                table: "Rating",
                columns: new[] { "UserId", "BookId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collection",
                table: "Collection",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookCollection",
                table: "BookCollection",
                columns: new[] { "BookId", "CollectionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookCollection_Books_BookId",
                table: "BookCollection",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCollection_Collection_CollectionId",
                table: "BookCollection",
                column: "CollectionId",
                principalTable: "Collection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collection_User_UserId",
                table: "Collection",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Books_BookId",
                table: "Rating",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_User_UserId",
                table: "Rating",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
