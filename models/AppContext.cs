using hall_of_fame.models;
using Microsoft.EntityFrameworkCore;

namespace Hall_of_fame.models
{
    public class AppContext : DbContext
    {
        public DbSet<Skill> Skills { get; set; } = null!;
        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<PersonSkill> PersonSkills { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=hallOfFame;Username=postgres;Password=3345");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Skills)
                .WithMany(s => s.Persons)
                .UsingEntity<PersonSkill>(
                    j => j
                        .HasOne(w => w.Skill)
                        .WithMany(t => t.PersonSkills)
                        .HasForeignKey(w => w.SkillId),
                    j => j
                        .HasOne(w => w.Person)
                        .WithMany(t => t.PersonSkills)
                        .HasForeignKey(w => w.PersonId),
                    j =>
                    {
                        j.Property(w => w.Level).HasDefaultValue(0);
                        j.HasKey(t => new { t.PersonId, t.SkillId });
                        j.ToTable("PersonSkill");
                    });
        }
    }
}
