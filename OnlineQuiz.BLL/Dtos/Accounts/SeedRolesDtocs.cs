﻿using Microsoft.AspNetCore.Identity;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Accounts
{
    public class SeedRolesDtocs
    {
        public static async Task SeedRoles(RoleManager<CustomRole> roleManager)
        {
            var roles = new List<string> {Roles.Admin, Roles.Instructor, Roles.Student };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new CustomRole { Name = role  });
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
}
