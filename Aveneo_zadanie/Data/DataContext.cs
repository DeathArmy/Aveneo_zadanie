using Aveneo_zadanie.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Aveneo_zadanie.Data
{
    public class DataContext : DbContext
    {
        public DbSet<EcbModel> EcbModels { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EcbModel>(entity =>
            {
                entity.HasKey(e => new {e.currency1, e.currency2, e.dataDate});
                entity.ToTable("ExchangeRates");
                entity.Property(e => e.currency1).HasColumnType("VARCHAR(5)");
                entity.Property(e => e.currency2).HasColumnType("VARCHAR(5)");
                entity.Property(e => e.dataDate).HasColumnType("DATE");
                entity.Property(e => e.exchangeRate).HasColumnType("REAL");
            });
            modelBuilder.Entity<ApiKey>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.ToTable("GeneratedKeys");
                entity.Property(e => e.id);
                entity.Property(e => e.key).HasColumnType("VARCHAR(32)");
            });
        }
    }
}
