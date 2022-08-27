using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatsWebHook.Migrations
{
    public partial class initialmysql3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "full_message",
                table: "messages",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "original_message",
                table: "messages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "original_message",
                table: "messages");

            migrationBuilder.UpdateData(
                table: "messages",
                keyColumn: "full_message",
                keyValue: null,
                column: "full_message",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "full_message",
                table: "messages",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
