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
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.UserDetail)
                   .WithOne(u => u.User)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Activities)
                   .WithMany(u => u.Users);

            User admin = new User()
            {
                UserID = 1,
                Email = "admin@gmail.com",
                Password = "admn",
                Role = "admn"
            };

            builder.HasData(admin);

            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.Email).HasColumnType("varchar").HasMaxLength(30);

            builder.Property(u => u.Password).HasColumnType("varchar").HasMaxLength(30);

            builder.Property(u => u.Role).HasColumnType("varchar").HasMaxLength(10);

        }
    }
}
