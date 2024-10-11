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
    public class QuestionsEntityTypeConfiguration : IEntityTypeConfiguration<Questions>
    {
        public void Configure(EntityTypeBuilder<Questions> builder)
        {
            builder.HasKey(q => q.Id);

        
            builder.Property(q => q.Tittle)
                   .IsRequired()
                   .HasMaxLength(3000);

         
            builder.Property(q => q.CorrectAnswer)
                   .IsRequired();


     }
    }
   
   
}
