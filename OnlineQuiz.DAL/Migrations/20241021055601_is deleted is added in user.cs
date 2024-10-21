using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineQuiz.DAL.Migrations
{
    /// <inheritdoc />
    public partial class isdeletedisaddedinuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "477684f6-7927-493b-a17b-47750dabb284");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "Age", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "Gender", "ImgUrl", "IsBanned", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "acb39a05-6d4f-41cd-86b5-02ff3e912a12", 0, "Mansura", 0, "a0cccee3-cbea-4cb0-984e-0035e1ffa8cd", "Users", "yossif155farouk@gmail.com", false, 0, null, false, false, false, null, null, null, "ZX12zx12#", null, false, "ecaa7b86-6c02-4e2a-8230-12ae7a0ebb59", false, "Yossif Farouk", 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "acb39a05-6d4f-41cd-86b5-02ff3e912a12");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "Age", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "Gender", "ImgUrl", "IsBanned", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "477684f6-7927-493b-a17b-47750dabb284", 0, "Mansura", 0, "57a37916-f587-4c2a-83c3-ac2bb495bc63", "Users", "yossif155farouk@gmail.com", false, 0, null, false, false, null, null, null, "WlgxMnp4MTIj", null, false, "dd2b17d5-2ef8-4ec4-995c-d56e573a9df9", false, "Yossif Farouk", 3 });
        }
    }
}
