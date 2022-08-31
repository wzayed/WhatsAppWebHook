using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatsWebHook.Migrations
{
    public partial class user_comment2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserComments_user_usrIdUser",
                table: "UserComments");

            migrationBuilder.DropIndex(
                name: "IX_UserComments_usrIdUser",
                table: "UserComments");

            migrationBuilder.DropColumn(
                name: "usrIdUser",
                table: "UserComments");

            migrationBuilder.CreateIndex(
                name: "IX_UserComments_IdUser",
                table: "UserComments",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_UserComments_user_IdUser",
                table: "UserComments",
                column: "IdUser",
                principalTable: "user",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserComments_user_IdUser",
                table: "UserComments");

            migrationBuilder.DropIndex(
                name: "IX_UserComments_IdUser",
                table: "UserComments");

            migrationBuilder.AddColumn<int>(
                name: "usrIdUser",
                table: "UserComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserComments_usrIdUser",
                table: "UserComments",
                column: "usrIdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_UserComments_user_usrIdUser",
                table: "UserComments",
                column: "usrIdUser",
                principalTable: "user",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
