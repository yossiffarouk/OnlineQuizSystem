using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineQuiz.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addAdminInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "acb39a05-6d4f-41cd-86b5-02ff3e912a12");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "Age", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "Gender", "ImgUrl", "IsBanned", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "6031ae75-5f3b-46e5-b2b3-851781d1ee46", 0, "Mansoura", 0, "2adc614e-123b-4951-8f00-56649873d0ef", "Admin", "yossif155farouk@gmail.com", true, 1, null, false, false, false, null, "YOSSIF155FAROUK@GMAIL.COM", "YOSSIF FAROUK", "ZX12zx12#", null, false, "0b23fee5-c61c-4485-824b-745d5b3b1ae2", false, "Yossif Farouk", 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6031ae75-5f3b-46e5-b2b3-851781d1ee46");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "Age", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "Gender", "ImgUrl", "IsBanned", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "acb39a05-6d4f-41cd-86b5-02ff3e912a12", 0, "Mansura", 0, "a0cccee3-cbea-4cb0-984e-0035e1ffa8cd", "Users", "yossif155farouk@gmail.com", false, 0, null, false, false, false, null, null, null, "ZX12zx12#", null, false, "ecaa7b86-6c02-4e2a-8230-12ae7a0ebb59", false, "Yossif Farouk", 3 });
        }
    }
}
