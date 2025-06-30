using LSE.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LSE.Infrastructure.DBContext
{
    public class LSEDBContext : DbContext
    {
        public LSEDBContext(DbContextOptions<LSEDBContext> options) : base(options)
        {
        }

        public DbSet<Broker> Broker { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Trade> Trade { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Value converter for char(36) <-> string
            var char36ToStringConverter = new ValueConverter<string, string>(
                v => v, 
                v => v  
            );

            // Stock entity configuration
            modelBuilder.Entity<Stock>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Stock>()
                .Property(s => s.TickerSymbol)
                .IsRequired()
                .HasMaxLength(16);

            // Broker entity configuration
            modelBuilder.Entity<Broker>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Broker>()
                .Property(b => b.Id)
                .HasColumnType("char(36)");

            modelBuilder.Entity<Broker>()
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Broker>()
                .Property(b => b.EmailId)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Broker>()
                .Property(b => b.Password)
                .IsRequired()
                .HasMaxLength(255);

            // Trade entity configuration
            modelBuilder.Entity<Trade>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Trade>()
                .Property(t => t.BrokerId)
                .IsRequired()
                .HasColumnType("char(36)");

            modelBuilder.Entity<Trade>()
                .Property(t => t.StockId)
                .IsRequired();

            modelBuilder.Entity<Trade>()
                .Property(t => t.Price)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<Trade>()
                .Property(t => t.NumberOfShares)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<Trade>()
                .Property(t => t.Timestamp)
                .IsRequired();

            modelBuilder.Entity<Trade>()
                .HasOne(t => t.Broker)
                .WithMany() 
                .HasForeignKey(t => t.BrokerId)
                .HasConstraintName("FK_Trade_Broker")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trade>()
                .HasOne(t => t.Stock)
                .WithMany(s => s.Trades)
                .HasForeignKey(t => t.StockId)
                .HasConstraintName("FK_Trade_Stock")
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);

        }
    }
}
