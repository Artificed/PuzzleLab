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
                        "00000000-0000-0000-0000-000000000000",
                        "admin",
                        "admin@gmail.com",
                        defaultPassword,
                        "Admin",
                        DateTime.UtcNow,
                        null
                    },
                    {
                        "11111111-1111-1111-1111-111111111111",
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
