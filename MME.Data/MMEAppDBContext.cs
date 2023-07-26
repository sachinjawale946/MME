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

            modelBuilder
       .Entity<StateModel>()
       .HasKey(d => d.StateId);
            modelBuilder.Entity<StateModel>().Property(x => x.StateId).ValueGeneratedOnAdd();

            modelBuilder
      .Entity<PincodeModel>()
      .HasKey(d => d.PinCodeId);
            modelBuilder.Entity<PincodeModel>().Property(x => x.PinCodeId).ValueGeneratedOnAdd();

            modelBuilder
      .Entity<LanguageModel>()
      .HasKey(d => d.LanguageId);
            modelBuilder.Entity<LanguageModel>().Property(x => x.LanguageId).ValueGeneratedOnAdd();

            modelBuilder
     .Entity<ReligionModel>()
     .HasKey(d => d.ReligionId);
            modelBuilder.Entity<ReligionModel>().Property(x => x.ReligionId).ValueGeneratedOnAdd();

            modelBuilder
    .Entity<CasteModel>()
    .HasKey(d => d.CasteId);
            modelBuilder.Entity<CasteModel>().Property(x => x.CasteId).ValueGeneratedOnAdd();

            modelBuilder
     .Entity<SubCasteModel>()
     .HasKey(d => d.SubCasteId);
                modelBuilder.Entity<SubCasteModel>().Property(x => x.SubCasteId).ValueGeneratedOnAdd();

            modelBuilder
    .Entity<OccupationModel>()
    .HasKey(d => d.OccupationId);
                modelBuilder.Entity<OccupationModel>().Property(x => x.OccupationId).ValueGeneratedOnAdd();

            modelBuilder
        .Entity<OccupationModel>()
        .HasKey(d => d.OccupationId);
            modelBuilder.Entity<OccupationModel>().Property(x => x.OccupationId).ValueGeneratedOnAdd();

            modelBuilder
        .Entity<EventModel>()
        .HasKey(d => d.EventId);
            modelBuilder.Entity<EventModel>().Property(x => x.EventId).ValueGeneratedOnAdd();

            modelBuilder
      .Entity<EventTypeModel>()
      .HasKey(d => d.EventTypeId);
            modelBuilder.Entity<EventTypeModel>().Property(x => x.EventTypeId).ValueGeneratedOnAdd();

            modelBuilder
      .Entity<EventFeedbackModel>()
      .HasKey(d => d.Id);
            modelBuilder.Entity<EventFeedbackModel>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder
     .Entity<ErrorLogModel>()
     .HasKey(d => d.Id);
            modelBuilder.Entity<ErrorLogModel>().Property(x => x.Id).ValueGeneratedOnAdd();

        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<StateModel> States { get; set; }
        public DbSet<PincodeModel> Pincodes { get; set; }
        public DbSet<LanguageModel> Languages { get; set; }
        public DbSet<ReligionModel> Religions { get; set; }
        public DbSet<CasteModel> Castes { get; set; }
        public DbSet<SubCasteModel> SubCastes { get; set; }
        public DbSet<OccupationModel> Occupations { get; set; }
        public DbSet<EventTypeModel> EventTypes { get; set; }
        public DbSet<EventModel> Events { get; set; }
        public DbSet<EventFeedbackModel> EventFeedbacks { get; set; }
        public DbSet<ErrorLogModel> ErrorLogs { get; set; }
    }
}