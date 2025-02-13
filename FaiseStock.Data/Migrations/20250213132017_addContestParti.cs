using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaiseStock.Data.Migrations
{
    /// <inheritdoc />
    public partial class addContestParti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contest_participants",
                columns: table => new
                {
                    contest_id = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_id = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    initial_balance = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    final_balance = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.contest_id, x.user_id });
                    table.ForeignKey(
                        name: "contest_participants_contest_id_foreign",
                        column: x => x.contest_id,
                        principalTable: "contest",
                        principalColumn: "contest_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "contest_participants_user_id_foreign",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "IX_contest_participants_user_id",
                table: "contest_participants",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contest_participants");
        }
    }
}
