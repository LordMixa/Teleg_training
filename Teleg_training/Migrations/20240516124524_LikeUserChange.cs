using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teleg_training.Migrations
{
    /// <inheritdoc />
    public partial class LikeUserChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "DBLikes",
                newName: "TGId");

            migrationBuilder.AlterColumn<string>(
                name: "Mode",
                table: "ProgramLists",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "ProgramLists",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_DBUsers_TGId",
                table: "DBUsers",
                column: "TGId");

            migrationBuilder.CreateIndex(
                name: "IX_DBLikes_TGId",
                table: "DBLikes",
                column: "TGId");

            migrationBuilder.AddForeignKey(
                name: "FK_DBLikes_DBUsers_TGId",
                table: "DBLikes",
                column: "TGId",
                principalTable: "DBUsers",
                principalColumn: "TGId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DBLikes_DBUsers_TGId",
                table: "DBLikes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_DBUsers_TGId",
                table: "DBUsers");

            migrationBuilder.DropIndex(
                name: "IX_DBLikes_TGId",
                table: "DBLikes");

            migrationBuilder.RenameColumn(
                name: "TGId",
                table: "DBLikes",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Mode",
                table: "ProgramLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "ProgramLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
