using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OnlineQuiz.DAL.Data.Configuration;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        // this method hash pass for admin add by admin - note better use fazwy algrothim for hash password
        private string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seeding the admin 
            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "Yossif Farouk",
                    Email = "yossif155farouk@gmail.com",
                    PasswordHash = "ZX12zx12#",
                    Adress = "Mansura",
                    Gender = 0,
                    UserType = UserTypeEnum.Admin,
                });

            // ins with students
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

