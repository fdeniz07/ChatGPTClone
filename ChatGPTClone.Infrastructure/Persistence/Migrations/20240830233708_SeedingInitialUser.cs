using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatGPTClone.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedingInitialUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedByUserId", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedByUserId", "ModifiedOn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("2798212b-3e5d-4556-8629-a64eb70da4a8"), 0, "ebb265f9-13dd-487b-928b-2229071c5624", "2798212b-3e5d-4556-8629-a64eb70da4a8", new DateTimeOffset(new DateTime(2024, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "fatih@gmail.com", true, "Fatih", "Deniz", false, null, null, null, "FATIH@GMAIL.COM", "FATIH", "AQAAAAIAAYagAAAAEBmz3df7lfROry+TruBJXSyGRlTM8kpXKrvFQ9WdZqjVOIK/bPFYS9hiewpi9jOzLw==", null, false, "0bea9d23-7d18-44cc-b622-596267c33b16", false, "fatih" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2798212b-3e5d-4556-8629-a64eb70da4a8"));
        }
    }
}
