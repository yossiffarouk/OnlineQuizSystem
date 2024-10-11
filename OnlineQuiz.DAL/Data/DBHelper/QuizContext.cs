using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineQuiz.DAL.Data.Configuration;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Data.DBHelper
{
    public class QuizContext :IdentityDbContext<Users>
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options) { }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        // this method hash pass for admin add by admin - note better use fazwy algrothim for hash password
        private string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

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

        public DbSet<Users> users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Quizzes> quizzes { get; set; }
        public DbSet<Questions> questions { get; set; }
        public DbSet<Answers> answers { get; set; }
        public DbSet<Attempts> attempts { get; set; }
        public DbSet<Tracks> tracks { get; set; }
        public DbSet<Option> Options { get; set; }
    


    }
}

