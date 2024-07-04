using Microsoft.EntityFrameworkCore;

namespace RunTracker.API.Data{

    public partial class RunTrackerDbContext : DbContext
    {
        public RunTrackerDbContext()
        {
        }

        public RunTrackerDbContext(DbContextOptions<RunTrackerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RunActivity> RunActivities { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RunActivity>(entity =>
            {
                entity.HasKey(e => e.RunId).HasName("PK__Run_Acti__E25F28757E9D4C80");

                entity.ToTable("Run_Activity");

                entity.Property(e => e.RunId).HasColumnName("Run_Id");
                entity.Property(e => e.DateTimeEnded).HasColumnType("datetime");
                entity.Property(e => e.DateTimeStarted).HasColumnType("datetime");
                entity.Property(e => e.Distance).HasColumnType("decimal(5, 2)");
                entity.Property(e => e.Location).HasMaxLength(100);
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User).WithMany(p => p.RunActivities)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_user");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACB4D75617");

                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.BirthDate).HasColumnType("datetime");
                entity.Property(e => e.Bmi)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("BMI");
                entity.Property(e => e.Height).HasColumnType("decimal(5, 2)");
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
