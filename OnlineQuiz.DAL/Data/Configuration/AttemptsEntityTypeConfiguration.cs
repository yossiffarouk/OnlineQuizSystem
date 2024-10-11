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
    public class AttemptsEntityTypeConfiguration : IEntityTypeConfiguration<Attempts>
    {
        public void Configure(EntityTypeBuilder<Attempts> builder)
        {
            builder.HasKey(a => a.Id);

            
            builder.Property(a => a.StartTime)
                   .IsRequired();

            builder.Property(a => a.EndTime)
                   .IsRequired();

            builder.Property(a => a.Score)
                   .IsRequired();

          builder
             .HasOne(a => a.Student)
             .WithMany(s => s.Attempts)  // Assuming Student has Attempts navigation property
             .HasForeignKey(a => a.StudentId)
             .OnDelete(DeleteBehavior.Cascade);


        }
            
    }
}
