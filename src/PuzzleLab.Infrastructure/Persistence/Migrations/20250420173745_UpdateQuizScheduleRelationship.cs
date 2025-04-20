using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuzzleLab.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuizScheduleRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_quizzes_schedule_id",
                table: "quizzes");

            migrationBuilder.CreateIndex(
                name: "ix_quizzes_schedule_id",
                table: "quizzes",
                column: "schedule_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_quizzes_schedule_id",
                table: "quizzes");

            migrationBuilder.CreateIndex(
                name: "ix_quizzes_schedule_id",
                table: "quizzes",
                column: "schedule_id");
        }
    }
}
