using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineQuiz.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateSeedDataToAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e683a00-2565-4b46-8b3b-6e398e62baf2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "Age", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "Gender", "ImgUrl", "IsBanned", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "9e683a00-2565-4b46-8b3b-6e398e62baf2", 0, "Mansoura", 0, "6be83a88-11a3-4896-b79c-2b7ba899afab", "Admin", "yossif155farouk@gmail.com", true, 1, null, false, false, false, null, "YOSSIF155FAROUK@GMAIL.COM", "YOSSIF FAROUK", "ZX12zx12#", null, false, "eee2bb25-ccc8-4242-bc7b-4beb72e29dd1", false, "Yossif Farouk", 3 });
        }
    }
}
