using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuzzleLab.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuizzes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_quizzes_schedules_schedule_id",
                table: "quizzes");

            migrationBuilder.AddForeignKey(
                name: "fk_quizzes_schedules_schedule_id",
                table: "quizzes",
                column: "schedule_id",
                principalTable: "schedules",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_quizzes_schedules_schedule_id",
                table: "quizzes");

            migrationBuilder.AddForeignKey(
                name: "fk_quizzes_schedules_schedule_id",
                table: "quizzes",
                column: "schedule_id",
                principalTable: "schedules",
                principalColumn: "id");
        }
    }
}
