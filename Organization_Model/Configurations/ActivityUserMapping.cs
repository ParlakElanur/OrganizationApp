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
    public class ActivityUserMapping : IEntityTypeConfiguration<ActivityUser>
    {
        public void Configure(EntityTypeBuilder<ActivityUser> builder)
        {
            builder.HasKey(a => new { a.ActivityID, a.UserID });

            builder.HasOne(a => a.Activity)
                   .WithMany(a => a.ActivityUsers)
                   .HasForeignKey(a => a.ActivityID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.User)
                .WithMany(a => a.ActivityUsers)
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
