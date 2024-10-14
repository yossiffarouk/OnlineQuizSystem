using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineQuiz.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addSoftDeleteToQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0fa83110-1a3a-4663-8d14-8ab077cc5bbc");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "questions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "Age", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "Gender", "ImgUrl", "IsBanned", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "a21dc925-ad6e-477f-ae1b-76570c85c0af", 0, "Mansura", 0, "4b1f7694-95d2-4672-915c-12a43dc102a3", "Users", "yossif155farouk@gmail.com", false, 0, null, false, false, null, null, null, "WlgxMnp4MTIj", null, false, "549a897c-90a5-4a0f-a9df-35539d36a974", false, "Yossif Farouk", 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a21dc925-ad6e-477f-ae1b-76570c85c0af");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "questions");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "Age", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "Gender", "ImgUrl", "IsBanned", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "0fa83110-1a3a-4663-8d14-8ab077cc5bbc", 0, "Mansura", 0, "b1357613-1e7b-4ce6-8f1c-ed08621bb93e", "Users", "yossif155farouk@gmail.com", false, 0, null, false, false, null, null, null, "WlgxMnp4MTIj", null, false, "7b15de95-5dc5-43b1-9c8c-1e1fe0759c95", false, "Yossif Farouk", 3 });
        }
    }
}
