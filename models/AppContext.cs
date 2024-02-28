using Microsoft.EntityFrameworkCore;

namespace Hall_of_fame.models
{
    public class AppContext : DbContext
    {
        public DbSet<Skill> Skills { get; set; } = null!;
        public DbSet<Person> Persons { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=hallOfFame;Username=postgres;Password=3345");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>()
                .HasOne(p => p.Person)
                .WithMany(s => s.Skills)
                .HasForeignKey(p => p.Id);
        }
    }
}
