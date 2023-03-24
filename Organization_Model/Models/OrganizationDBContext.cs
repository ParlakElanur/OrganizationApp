using Microsoft.EntityFrameworkCore;
using Organization_Model.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization_Model.Models
{
    public class OrganizationDBContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=OrganizationDb;Trusted_Connection=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new UserDetailMapping());
            modelBuilder.ApplyConfiguration(new ActivityMapping());
            modelBuilder.ApplyConfiguration(new CategoryMapping());
        }
    }
}
