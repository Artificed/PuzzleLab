using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuzzleLab.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var defaultPassword = BCrypt.Net.BCrypt.HashPassword("password");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "username", "password", "role" },
                values: new object[,]
                {
                    { Guid.NewGuid(), "admin@gmail.com", "admin", defaultPassword, "admin" },
                    { Guid.NewGuid(), "user1@gmail.com", "user1", defaultPassword, "user" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "email",
                keyValues: new object[] { "admin@gmail.com", "user1@gmail.com" });
        }
    }
}
