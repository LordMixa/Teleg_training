using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teleg_training.Migrations
{
    /// <inheritdoc />
    public partial class inttolong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DBLikes_DBUsers_UserId",
                table: "DBLikes");

            migrationBuilder.DropIndex(
                name: "IX_DBLikes_UserId",
                table: "DBLikes");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
