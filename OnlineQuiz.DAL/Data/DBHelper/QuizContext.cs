using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OnlineQuiz.DAL.Data.Configuration;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlineQuiz.DAL.Data.DBHelper
{
    public class QuizContext : IdentityDbContext<Users, CustomRole, string>
    {
        public QuizContext (DbContextOptions<QuizContext> options)
            : base(options)
        {
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies(true);
        }
        
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Tracks>().HasData(
            //             new Tracks
            //             {
            //                 Id = 4,
            //                 Name = "Bio",
            //                 IsDeleted = false
            //             },
            //             new Tracks
            //             {
            //                 Id = 5,
            //                 Name = "Math",
            //                 IsDeleted = false
            //             },
            //             new Tracks
            //             {
            //                 Id = 6,
            //                 Name = "Chemistry", // Corrected spelling of "Chemistry"
            //                 IsDeleted = false
            //             },
            //             new Tracks
            //             {
            //                 Id = 7,
            //                 Name = "Programming", // Corrected spelling of "Programming"
            //                 IsDeleted = false
            //             },
            //             new Tracks
            //             {
            //                 Id = 8,
            //                 Name = "Statics",
            //                 IsDeleted = false
            //             },
            //             new Tracks
            //             {
            //                 Id = 9,
            //                 Name = "Arabic", // Corrected spelling of "Arabic"
            //                 IsDeleted = false
            //             }
            //         ) ;

            //var adminUserId = Guid.NewGuid().ToString();
            //modelBuilder.Entity<Admin>().HasData(
            //new Admin
            //{
            //    Id = adminUserId, // Ensure to set the Id for IdentityUser
            //    UserName = "Yossif Farouk",
            //    Email = "yossif155farouk@gmail.com",
            //    NormalizedEmail = "YOSSIF155FAROUK@GMAIL.COM", // Optional, but recommended
            //    NormalizedUserName = "YOSSIF FAROUK", // Optional, but recommended
            //    EmailConfirmed = true, // Set to true if the email is confirmed
            //    PasswordHash = "ZX12zx12#", // Replace with a hashed password in production
            //    Adress = "Mansoura",
            //    Gender = GenderType.Male, // Use enum for gender
            //    UserType = UserTypeEnum.Admin,
            //    IsBanned = false,
            //    IsDeleted = false
            //});

            //modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            //new IdentityUserRole<string>
            //  {
            //         UserId = adminUserId,
            //         RoleId = adminRoleId // This will be replaced by the actual ID after migration
            //  }
            //);



            #region instructors  with students
            //instructors  with students
            modelBuilder.Entity<StudentInstructor>()
                        .HasKey(si => new { si.StudentId, si.InstructorId }); // Composite key

            modelBuilder.Entity<StudentInstructor>()
                .HasOne(si => si.Student)
                .WithMany(s => s.StudentInstructors)
                .HasForeignKey(si => si.StudentId);

            modelBuilder.Entity<StudentInstructor>()
                .HasOne(si => si.Instructor)
                .WithMany(i => i.StudentInstructors)
                .HasForeignKey(si => si.InstructorId); 
            #endregion


            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AnswersEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AttemptsEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InstructorEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OptionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionsEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuizzesEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TracksEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UsersEntityTypeConfiguration());

            
         
        }

        public virtual DbSet<Users> users { get; set; }
        public  DbSet<Student> Students { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Quizzes> quizzes { get; set; }
        public virtual DbSet<Questions> questions { get; set; }
        public virtual DbSet<Answers> answers { get; set; }
        public virtual DbSet<Attempts> attempts { get; set; }
        public virtual DbSet<Tracks> tracks { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<StudentInstructor> StudentInstructors { get; set; }
        public virtual DbSet<CustomRole> CustomRoles { get; set; }



    }
}

