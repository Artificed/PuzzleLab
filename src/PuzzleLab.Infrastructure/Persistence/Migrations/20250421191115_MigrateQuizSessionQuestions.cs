using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuzzleLab.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MigrateQuizSessionQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "quiz_session_questions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    quiz_session_id = table.Column<Guid>(type: "uuid", nullable: false),
                    question_id = table.Column<Guid>(type: "uuid", nullable: false),
                    question_order = table.Column<int>(type: "integer", nullable: false),
                    answered = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quiz_session_questions", x => x.id);
                    table.ForeignKey(
                        name: "fk_quiz_session_questions_questions_question_id",
                        column: x => x.question_id,
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_quiz_session_questions_quiz_sessions_quiz_session_id",
                        column: x => x.quiz_session_id,
                        principalTable: "quiz_sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_quiz_session_questions_question_id",
                table: "quiz_session_questions",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_quiz_session_questions_quiz_session_id",
                table: "quiz_session_questions",
                column: "quiz_session_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "quiz_session_questions");
        }
    }
}
