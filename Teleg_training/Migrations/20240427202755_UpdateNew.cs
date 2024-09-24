using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teleg_training.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DBLikes_DBUsers_UserId1",
                table: "DBLikes");

            migrationBuilder.DropIndex(
                name: "IX_DBLikes_UserId1",
                table: "DBLikes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "DBLikes");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "TGId",
                table: "DBUsers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DBUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "DBLikes",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_DBLikes_UserId",
                table: "DBLikes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DBLikes_DBUsers_UserId",
                table: "DBLikes",
                column: "UserId",
                principalTable: "DBUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DBLikes_DBUsers_UserId",
                table: "DBLikes");

            migrationBuilder.DropIndex(
                name: "IX_DBLikes_UserId",
                table: "DBLikes");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TGId",
                table: "DBUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DBUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "DBLikes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "DBLikes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DBLikes_UserId1",
                table: "DBLikes",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DBLikes_DBUsers_UserId1",
                table: "DBLikes",
                column: "UserId1",
                principalTable: "DBUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
