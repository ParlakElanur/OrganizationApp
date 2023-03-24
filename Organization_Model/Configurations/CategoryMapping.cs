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
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(c => c.Activities)
                   .WithOne(c => c.Category)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(c => c.CategoryName).IsUnique();

            builder.Property(c => c.CategoryName).HasColumnType("varchar").HasMaxLength(40);

        }
    }
}
