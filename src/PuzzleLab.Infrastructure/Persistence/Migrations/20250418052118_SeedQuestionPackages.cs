using Microsoft.EntityFrameworkCore.Migrations;
using PuzzleLab.Infrastructure.Persistence.Utils;

#nullable disable

namespace PuzzleLab.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedQuestionPackages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var packageId = new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef");
            var adminUserId = new Guid("00000000-0000-0000-0000-000000000000");
            var createdAt = DateTime.UtcNow;
            var lastModifiedAt = DateTime.UtcNow;

            string imageRelativePath = "Persistence/SeedData/webdev-quiz.jpg";

            byte[] seedImageData;
            try
            {
                seedImageData = SeedHelper.LoadImageDataFromFile(imageRelativePath);
                if (seedImageData == null || seedImageData.Length == 0)
                {
                    throw new InvalidOperationException($"Failed to load seed image data from '{imageRelativePath}'. Check file existence and build properties.");
                }
            } catch (Exception ex)
            {
                throw new InvalidOperationException($"Error loading seed image data from '{imageRelativePath}'. See inner exception.", ex);
            }

            migrationBuilder.InsertData(
                table: "question_packages",
                columns: new[] {
                    "id",
                    "name",
                    "description",
                    "duration_in_minutes",
                    "image_data",
                    "created_by_id",
                    "created_at",
                    "last_modified_at"
                },
                values: new object[] {
                    packageId,
                    "General Knowledge Fun Pack",
                    "A starter pack with questions covering various topics.",
                    15,
                    seedImageData,
                    adminUserId,
                    createdAt,
                    lastModifiedAt
                });
        }

        /// <inheritdoc />
       protected override void Down(MigrationBuilder migrationBuilder)
        {
             var packageId = new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef");

            migrationBuilder.DeleteData(
                table: "question_packages",
                keyColumn: "id",
                keyValue: packageId);
        }
    }
}
