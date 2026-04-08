using Microsoft.EntityFrameworkCore;
using TicketingSystem.Models;

namespace TicketingSystem.Data
{
    /// <summary>
    /// Application Database Context
    /// Handles all DB tables and configurations
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Constructor for dependency injection
        /// </summary>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        /// <summary>
        /// Users table
        /// </summary>
        public DbSet<User> Users { get; set; } = null!;

        /// <summary>
        /// Tickets table
        /// </summary>
        public DbSet<Ticket> Tickets { get; set; } = null!;

        /// <summary>
        /// Ticket comments table
        /// </summary>
        public DbSet<TicketComment> TicketComments { get; set; } = null!;

        /// <summary>
        /// Model configurations (Fluent API)
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================
            // Users table configurations
            // ==========================
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>(); // Enum -> string

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // ==========================
            // Tickets table configurations
            // ==========================
            modelBuilder.Entity<Ticket>()
                .Property(t => t.Status)
                .HasConversion<string>()         // Enum -> string
                .HasDefaultValue(TicketStatus.Open);

            modelBuilder.Entity<Ticket>()
                .Property(t => t.Priority)
                .HasConversion<int>()            // Enum -> int
                .HasDefaultValue(TicketPriority.Medium);

            modelBuilder.Entity<Ticket>()
                .Property(t => t.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Ticket>()
                .Property(t => t.UpdatedAt)
                .IsRequired(false);

            modelBuilder.Entity<Ticket>()
                .Property(t => t.ClosedAt)
                .IsRequired(false);

            // ==========================
            // TicketComments table configurations
            // ==========================
            modelBuilder.Entity<TicketComment>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<TicketComment>()
                .HasOne(c => c.Ticket)
                .WithMany() // You can add navigation property in Ticket if needed
                .HasForeignKey(c => c.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TicketComment>()
                .HasOne(c => c.User)
                .WithMany() // You can add navigation property in User if needed
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}