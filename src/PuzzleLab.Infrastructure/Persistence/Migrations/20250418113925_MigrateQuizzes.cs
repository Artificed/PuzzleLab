using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuzzleLab.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MigrateQuizzes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_schedules_question_packages_question_package_id",
                table: "schedules");

            migrationBuilder.DropIndex(
                name: "ix_schedules_question_package_id",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "question_package_id",
                table: "schedules");

            migrationBuilder.CreateTable(
                name: "quizzes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    question_package_id = table.Column<Guid>(type: "uuid", nullable: false),
                    schedule_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quizzes", x => x.id);
                    table.ForeignKey(
                        name: "fk_quizzes_question_packages_question_package_id",
                        column: x => x.question_package_id,
                        principalTable: "question_packages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_quizzes_schedules_schedule_id",
                        column: x => x.schedule_id,
                        principalTable: "schedules",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_quizzes_question_package_id",
                table: "quizzes",
                column: "question_package_id");

            migrationBuilder.CreateIndex(
                name: "ix_quizzes_schedule_id",
                table: "quizzes",
                column: "schedule_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "quizzes");

            migrationBuilder.AddColumn<Guid>(
                name: "question_package_id",
                table: "schedules",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_schedules_question_package_id",
                table: "schedules",
                column: "question_package_id");

            migrationBuilder.AddForeignKey(
                name: "fk_schedules_question_packages_question_package_id",
                table: "schedules",
                column: "question_package_id",
                principalTable: "question_packages",
                principalColumn: "id");
        }
    }
}
