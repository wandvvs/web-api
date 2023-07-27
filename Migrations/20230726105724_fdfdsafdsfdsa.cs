using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class fdfdsafdsfdsa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_users_AuthorId",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_posts_AuthorId",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "posts");

            migrationBuilder.AddColumn<string>(
                name: "AuthorLogin",
                table: "posts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorLogin",
                table: "posts");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_posts_AuthorId",
                table: "posts",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_users_AuthorId",
                table: "posts",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
