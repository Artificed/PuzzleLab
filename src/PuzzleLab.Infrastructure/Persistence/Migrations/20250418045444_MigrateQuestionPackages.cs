using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuzzleLab.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MigrateQuestionPackages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "question_packages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    duration_in_minutes = table.Column<int>(type: "integer", nullable: false),
                    image_data = table.Column<byte[]>(type: "bytea", nullable: false),
                    created_by_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_question_packages", x => x.id);
                    table.ForeignKey(
                        name: "fk_question_packages_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_question_packages_created_by_id",
                table: "question_packages",
                column: "created_by_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "question_packages");
        }
    }
}
