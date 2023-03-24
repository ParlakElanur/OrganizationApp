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
    public class UserDetailMapping : IEntityTypeConfiguration<UserDetail>
    {
        public void Configure(EntityTypeBuilder<UserDetail> builder)
        {
            builder.HasKey(u => u.UserID);

            builder.Property(u => u.Name).HasColumnType("varchar").HasMaxLength(30);

            builder.Property(u => u.Surname).HasColumnType("varchar").HasMaxLength(30);

        }
    }
}
