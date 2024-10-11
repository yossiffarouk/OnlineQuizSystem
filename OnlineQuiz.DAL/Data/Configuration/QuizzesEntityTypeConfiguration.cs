using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Data.Configuration
{
    public class QuizzesEntityTypeConfiguration : IEntityTypeConfiguration<Quizzes>
    {
        public void Configure(EntityTypeBuilder<Quizzes> builder)
        {
            // Primary Key
            builder.HasKey(q => q.Id); 

            // Properties 
            builder.Property(q => q.Tittle)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(q => q.Description)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(q => q.QuizDegree)
                   .IsRequired();

            // Default to current date
            builder.Property(q => q.CreatedDate)
                   .HasDefaultValueSql("GETDATE()"); 

            builder.Property(q => q.ExpireDate)
                   .IsRequired();

            builder.Property(q => q.ExamTime)
                   .IsRequired();

            // Quizzes -> Instructor (many-to-one relationship)
           builder
                .HasOne(q => q.Instructor)
                .WithMany(i => i.quizzes)  // Assuming the Instructor class has a collection of Quizzes
                .HasForeignKey(q => q.InstructorId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: Configure delete behavior

          
        }
    }

  
}
