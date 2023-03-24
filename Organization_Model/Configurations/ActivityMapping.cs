using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Organization_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization_Model.Configurations
{
    public class ActivityMapping : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.HasOne(a => a.Category)
                   .WithMany(a => a.Activities)
                   .HasForeignKey(a => a.CategoryID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(a => a.Name).HasColumnType("varchar").HasMaxLength(50);

            builder.Property(a => a.Detail).HasColumnType("varchar").HasMaxLength(600);

            builder.Property(a => a.Date).HasColumnType("datetime2(7)");

            builder.Property(a => a.ActivityDeadline).HasColumnType("datetime2(7)");

            builder.Property(a => a.City).HasColumnType("varchar").HasMaxLength(20);

            builder.Property(a => a.Address).HasColumnType("varchar").HasMaxLength(300);

            builder.Property(a => a.Quota).HasColumnType("int");

            builder.Property(a => a.Status).HasColumnType("varchar").HasMaxLength(1);

            builder.HasCheckConstraint("CK_DateDeadline", "DATEDIFF(DAY,[ActivityDeadline],[Date])>=1");
        }

    }
}
