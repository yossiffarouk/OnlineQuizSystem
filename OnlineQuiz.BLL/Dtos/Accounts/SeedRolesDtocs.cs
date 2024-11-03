using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineQuiz.DAL.Data.Models;

namespace OnlineQuiz.BLL.Dtos.Accounts
{
    public class SeedRolesDtocs
    {
        public static async Task SeedRoles(RoleManager<CustomRole> roleManager, ILogger logger)
        {
            var roles = new List<string> { Roles.Admin, Roles.Instructor, Roles.Student };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new CustomRole { Name = role });
                    if (result.Succeeded)
                    {
                        logger.LogInformation($"Role {role} created successfully.");
                    }
                    else
                    {
                        logger.LogError($"Error creating role {role}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }
        }
    }

    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Instructor = "Instructor";
        public const string Student = "Student";
    }

    public class SeedAdminUserdtos
    {
        public static async Task SeedAdminUser(UserManager<Users> userManager, RoleManager<CustomRole> roleManager, ILogger logger)
        {
            const string adminEmail = "yossif155farouk@gmail.com"; // Consider moving to configuration
            const string adminPassword = "ZX12zx12#"; // Consider moving to configuration

            var existingAdminUser = await userManager.FindByEmailAsync(adminEmail);
            if (existingAdminUser == null)
            {
                var adminUser = new OnlineQuiz.DAL.Data.Models.Admin
                {
                    UserName = "Yossif Farouk",
                    Email = adminEmail,
                    NormalizedEmail = adminEmail.ToUpper(),
                    NormalizedUserName = "YOSSIF FAROUK",
                    EmailConfirmed = true,
                    Adress = "Mansoura",
                    Gender = GenderType.Male,
                    Age = 20,
                    UserType = UserTypeEnum.Admin,
                    IsBanned = false,
                    IsDeleted = false
                };

                var createAdminResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdminResult.Succeeded)
                {
                    var adminRole = await roleManager.FindByNameAsync(Roles.Admin);
                    if (adminRole != null)
                    {
                        await userManager.AddToRoleAsync(adminUser, Roles.Admin);
                        logger.LogInformation($"Admin user {adminUser.UserName} created and added to {Roles.Admin} role.");
                    }
                }
                else
                {
                    logger.LogError($"Error creating admin user: {string.Join(", ", createAdminResult.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                logger.LogInformation("Admin user already exists.");
            }
        }
    }
}
