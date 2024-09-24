using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teleg_training.Migrations
{
    /// <inheritdoc />
    public partial class Like : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "ProgramLists");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProgramLists",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "DBUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TGId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBUsers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "DBLikes",
                columns: table => new
                {
                    LikeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProgramListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBLikes", x => x.LikeId);
                    table.ForeignKey(
                        name: "FK_DBLikes_DBUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "DBUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DBLikes_ProgramLists_ProgramListId",
                        column: x => x.ProgramListId,
                        principalTable: "ProgramLists",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramLists_Name",
                table: "ProgramLists",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DBLikes_ProgramListId",
                table: "DBLikes",
                column: "ProgramListId");

            migrationBuilder.CreateIndex(
                name: "IX_DBLikes_UserId",
                table: "DBLikes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DBLikes");

            migrationBuilder.DropTable(
                name: "DBUsers");

            migrationBuilder.DropIndex(
                name: "IX_ProgramLists_Name",
                table: "ProgramLists");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProgramLists",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "ProgramLists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
