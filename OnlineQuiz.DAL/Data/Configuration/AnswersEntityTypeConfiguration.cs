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
    public class AnswersEntityTypeConfiguration : IEntityTypeConfiguration<Answers>
    {
        public void Configure(EntityTypeBuilder<Answers> builder)
        {
          
            builder.HasKey(a => a.Id);

         
            builder.Property(a => a.SubmittedAnswer)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.IsCorrect)
                    .IsRequired()
                    .HasDefaultValue(0);

        }
    }
}
