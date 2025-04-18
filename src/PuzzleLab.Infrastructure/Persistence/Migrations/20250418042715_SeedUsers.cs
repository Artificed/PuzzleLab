using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuzzleLab.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        string defaultPassword = BCrypt.Net.BCrypt.HashPassword("password");

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "username", "email", "password_hash", "role", "created_at", "last_login_at" },
                values: new object[,]
                {
                    {
                        Guid.NewGuid(),
                        "admin",
                        "admin@gmail.com",
                        defaultPassword,
                        "Admin",
                        DateTime.UtcNow,
                        null
                    },
                    {
                        Guid.NewGuid(),
                        "user1",
                        "user1@gmail.com",
                        defaultPassword,
                        "User",
                        DateTime.UtcNow,
                        null
                    }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.DeleteData(
                 table: "users",
                 keyColumn: "username",
                 keyValues: new object[] { "admin", "user1" }
             );

        }
    }
}
