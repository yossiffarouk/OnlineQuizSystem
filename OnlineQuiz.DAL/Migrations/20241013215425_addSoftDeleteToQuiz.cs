using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineQuiz.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addSoftDeleteToQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e01f6155-5d4a-4269-b382-8272777b7d28");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "quizzes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "Age", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "Gender", "ImgUrl", "IsBanned", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "0acd4bf3-d930-48ae-aa75-89eb8b742259", 0, "Mansura", 0, "b7f181c3-e73c-42b7-92cd-6fb334c85ee2", "Users", "yossif155farouk@gmail.com", false, 0, null, false, false, null, null, null, "WlgxMnp4MTIj", null, false, "5a823c38-0a0a-42c9-8ef2-6ecf7448efcb", false, "Yossif Farouk", 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0acd4bf3-d930-48ae-aa75-89eb8b742259");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "quizzes");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "Age", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "Gender", "ImgUrl", "IsBanned", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "e01f6155-5d4a-4269-b382-8272777b7d28", 0, "Mansura", 0, "a4278a69-829a-4623-b0d9-b018102326be", "Users", "yossif155farouk@gmail.com", false, 0, null, false, false, null, null, null, "WlgxMnp4MTIj", null, false, "efaba414-18f8-4147-9976-8d9c676e8c84", false, "Yossif Farouk", 3 });
        }
    }
}
