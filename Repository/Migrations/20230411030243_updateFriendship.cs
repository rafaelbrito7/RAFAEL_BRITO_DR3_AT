using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class updateFriendship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_People_FriendId",
                table: "Friendships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships");

            migrationBuilder.RenameColumn(
                name: "FriendId",
                table: "Friendships",
                newName: "BPersonId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Friendships",
                newName: "APersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendships_FriendId",
                table: "Friendships",
                newName: "IX_Friendships_BPersonId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Friendships",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_APersonId",
                table: "Friendships",
                column: "APersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_People_APersonId",
                table: "Friendships",
                column: "APersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_People_BPersonId",
                table: "Friendships",
                column: "BPersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_People_APersonId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_People_BPersonId",
                table: "Friendships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_APersonId",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Friendships");

            migrationBuilder.RenameColumn(
                name: "BPersonId",
                table: "Friendships",
                newName: "FriendId");

            migrationBuilder.RenameColumn(
                name: "APersonId",
                table: "Friendships",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendships_BPersonId",
                table: "Friendships",
                newName: "IX_Friendships_FriendId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships",
                columns: new[] { "PersonId", "FriendId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_People_FriendId",
                table: "Friendships",
                column: "FriendId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
