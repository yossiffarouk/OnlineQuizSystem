using Microsoft.EntityFrameworkCore;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Data.Configuration
{
    public class OptionEntityTypeConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Option> builder)
        {

            builder.HasKey(o => o.OptionId);
            builder.Property(o => o.OptionText)
                   .IsRequired()
                   .HasMaxLength(500);            

           
        }
    }
}
