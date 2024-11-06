using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineQuiz.DAL.Migrations
{
    /// <inheritdoc />
    public partial class finail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6031ae75-5f3b-46e5-b2b3-851781d1ee46");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "Age", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "Gender", "ImgUrl", "IsBanned", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "de8808f0-61e4-47b6-8949-7b30a78e2488", 0, "Mansoura", 0, "210b0bfb-7521-4ebe-9d1b-4d04e5568b4a", "Admin", "yossif155farouk@gmail.com", true, 1, null, false, false, false, null, "YOSSIF155FAROUK@GMAIL.COM", "YOSSIF FAROUK", "ZX12zx12#", null, false, "96f44b47-5dbf-4ebc-8cca-48d751e8e82f", false, "Yossif Farouk", 3 });

            migrationBuilder.InsertData(
                table: "tracks",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 4, false, "Bio" },
                    { 5, false, "Math" },
                    { 6, false, "Chemistry" },
                    { 7, false, "Programming" },
                    { 8, false, "Statics" },
                    { 9, false, "Arabic" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "de8808f0-61e4-47b6-8949-7b30a78e2488");

            migrationBuilder.DeleteData(
                table: "tracks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tracks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tracks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "tracks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "tracks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "tracks",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "Age", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "Gender", "ImgUrl", "IsBanned", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "6031ae75-5f3b-46e5-b2b3-851781d1ee46", 0, "Mansoura", 0, "2adc614e-123b-4951-8f00-56649873d0ef", "Admin", "yossif155farouk@gmail.com", true, 1, null, false, false, false, null, "YOSSIF155FAROUK@GMAIL.COM", "YOSSIF FAROUK", "ZX12zx12#", null, false, "0b23fee5-c61c-4485-824b-745d5b3b1ae2", false, "Yossif Farouk", 3 });
        }
    }
}
