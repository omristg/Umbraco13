using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Umbraco13Test.Migrations
{
    /// <inheritdoc />
    public partial class FluetTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogComments",
                table: "BlogComments");

            migrationBuilder.RenameTable(
                name: "BlogComments",
                newName: "blogComment");

            migrationBuilder.RenameColumn(
                name: "Website",
                table: "blogComment",
                newName: "website");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "blogComment",
                newName: "message");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "blogComment",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "BlogPostUmbracoKey",
                table: "blogComment",
                newName: "blogPostUmbracoKey");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "blogComment",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_blogComment",
                table: "blogComment",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_blogComment",
                table: "blogComment");

            migrationBuilder.RenameTable(
                name: "blogComment",
                newName: "BlogComments");

            migrationBuilder.RenameColumn(
                name: "website",
                table: "BlogComments",
                newName: "Website");

            migrationBuilder.RenameColumn(
                name: "message",
                table: "BlogComments",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "BlogComments",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "blogPostUmbracoKey",
                table: "BlogComments",
                newName: "BlogPostUmbracoKey");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "BlogComments",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogComments",
                table: "BlogComments",
                column: "Id");
        }
    }
}
