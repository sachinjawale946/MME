using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using MME.Model.Shared;

namespace MME.Data
{
    public class MMEAppDBContext : DbContext
    {
        public MMEAppDBContext(DbContextOptions<MMEAppDBContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
         .Entity<UserModel>()
         .HasKey(d => d.UserId);
            modelBuilder.Entity<UserModel>().Property(x => x.UserId).ValueGeneratedOnAdd();

            modelBuilder
         .Entity<RoleModel>()
         .HasKey(d => d.RoleId);
            modelBuilder.Entity<RoleModel>().Property(x => x.RoleId).ValueGeneratedOnAdd();

        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
    }
}