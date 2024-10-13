using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineQuiz.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addSoftDeleteToTrack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0acd4bf3-d930-48ae-aa75-89eb8b742259");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "tracks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "Age", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "Gender", "ImgUrl", "IsBanned", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "0fa83110-1a3a-4663-8d14-8ab077cc5bbc", 0, "Mansura", 0, "b1357613-1e7b-4ce6-8f1c-ed08621bb93e", "Users", "yossif155farouk@gmail.com", false, 0, null, false, false, null, null, null, "WlgxMnp4MTIj", null, false, "7b15de95-5dc5-43b1-9c8c-1e1fe0759c95", false, "Yossif Farouk", 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0fa83110-1a3a-4663-8d14-8ab077cc5bbc");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "tracks");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "Age", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "Gender", "ImgUrl", "IsBanned", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "0acd4bf3-d930-48ae-aa75-89eb8b742259", 0, "Mansura", 0, "b7f181c3-e73c-42b7-92cd-6fb334c85ee2", "Users", "yossif155farouk@gmail.com", false, 0, null, false, false, null, null, null, "WlgxMnp4MTIj", null, false, "5a823c38-0a0a-42c9-8ef2-6ecf7448efcb", false, "Yossif Farouk", 3 });
        }
    }
}
