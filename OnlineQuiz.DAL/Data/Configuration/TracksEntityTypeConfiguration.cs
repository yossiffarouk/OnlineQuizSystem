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
    public class TracksEntityTypeConfiguration : IEntityTypeConfiguration<Tracks>
    {
        public void Configure(EntityTypeBuilder<Tracks> builder)
        {
            builder.HasKey(t => t.Id);

            // تقييد الحقل Name كحقل مطلوب
            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(200); // يمكنك تعيين الطول الأقصى حسب احتياجاتك

           
        }
    }
}
