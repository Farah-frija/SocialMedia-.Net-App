using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialMigrations6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceiverId1",
                table: "FriendRequest",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderId1",
                table: "FriendRequest",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequest_ReceiverId1",
                table: "FriendRequest",
                column: "ReceiverId1");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequest_SenderId1",
                table: "FriendRequest",
                column: "SenderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequest_AspNetUsers_ReceiverId1",
                table: "FriendRequest",
                column: "ReceiverId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequest_AspNetUsers_SenderId1",
                table: "FriendRequest",
                column: "SenderId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequest_AspNetUsers_ReceiverId1",
                table: "FriendRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequest_AspNetUsers_SenderId1",
                table: "FriendRequest");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequest_ReceiverId1",
                table: "FriendRequest");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequest_SenderId1",
                table: "FriendRequest");

            migrationBuilder.DropColumn(
                name: "ReceiverId1",
                table: "FriendRequest");

            migrationBuilder.DropColumn(
                name: "SenderId1",
                table: "FriendRequest");
        }
    }
}
