using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace API_Arcadia.Models.Data
{
    public class ContextArcadia : DbContext
    {
        public ContextArcadia(DbContextOptions<ContextArcadia> options) : base(options)
        {

        }

        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<AnimalImage> AnimalImages { get; set; }
        public virtual DbSet<Diet> Diets { get; set; }
        public virtual DbSet<Habitat> Habitats { get; set; }
        public virtual DbSet<HabitatImage> HabitatImages { get; set; }
        public virtual DbSet<Health> Healths { get; set; }
        public virtual DbSet<OpeningHour> OpeningHours { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<SizeUnit> SizeUnits { get; set; }
        public virtual DbSet<WeightUnit> WeightUnits { get; set; }
        public virtual DbSet<Species> Speciess { get; set; }
        public virtual DbSet<SpeciesHabitat> SpeciesHabitats { get; set; }
        public virtual DbSet<VetVisit> VetVisits { get; set; }
        public virtual DbSet<ZooService> ZooServices { get; set; }
        public virtual DbSet<EmployeeFeeding> EmployeeFeedings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name).HasMaxLength(50);

                entity.HasOne(a => a.SpeciesData).WithMany().HasForeignKey(a => a.IdSpecies).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(a => a.HealthData).WithMany().HasForeignKey(a => a.IdHealth).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<AnimalImage>(entity =>
            {
                entity.HasKey(ai => ai.Id);
                entity.Property(ai => ai.Slug).HasMaxLength(150);
                entity.Property(hi => hi.MiniSlug).HasMaxLength(150); 

                entity.HasOne<Animal>().WithMany(a => a.Pics).HasForeignKey(ai => ai.IdAnimal);
            });

            modelBuilder.Entity<Diet>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Habitat>(entity =>
            {
                entity.HasKey(h => h.Id);
                entity.Property(h => h.Name).HasMaxLength(100);

                entity.HasMany<Species>().WithMany(s => s.habitats).UsingEntity<SpeciesHabitat>(
                    l => l.HasOne<Species>().WithMany().HasForeignKey(sh => sh.IdSpecies),
                    r => r.HasOne<Habitat>().WithMany().HasForeignKey(sh => sh.IdHabitat));
            });

            modelBuilder.Entity<HabitatImage>(entity =>
            {
                entity.HasKey(hi => hi.Id);
                entity.Property(hi => hi.Slug).HasMaxLength(150);
                entity.Property(hi => hi.MiniSlug).HasMaxLength(150);

                entity.HasOne<Habitat>().WithMany(h => h.Pics).HasForeignKey(hi => hi.IdHabitat);
            });

            modelBuilder.Entity<Health>(entity =>
            {
                entity.HasKey(h => h.Id);
                entity.Property(h => h.State).HasMaxLength(20);
            });

            modelBuilder.Entity<OpeningHour>(entity =>
            {
                entity.HasKey(oh => oh.Id);
                entity.Property(oh => oh.DayOfWeek).HasMaxLength(10);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Pseudo).HasMaxLength(100);
            });

            modelBuilder.Entity<SizeUnit>(entity =>
            {
                entity.HasKey(su => su.Id);
                entity.Property(su => su.Name).HasMaxLength(20);
                entity.Property(su => su.Abbr).HasMaxLength(2);
            });

            modelBuilder.Entity<Species>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).HasMaxLength(150);
                entity.Property(s => s.ScientificName).HasMaxLength(150);

                entity.HasOne(s => s.diet).WithMany().HasForeignKey(s => s.IdDiet).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(s => s.sizeUnit).WithMany().HasForeignKey(s => s.IdSizeUnit).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(s => s.weightUnit).WithMany().HasForeignKey(s => s.IdWeightUnit).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<SpeciesHabitat>(entity =>
            {
                entity.HasKey(sh => new { sh.IdHabitat, sh.IdSpecies });
            });

            modelBuilder.Entity<VetVisit>(entity =>
            {
                entity.HasKey(vv => vv.Id);
                entity.Property(vv => vv.Food).HasMaxLength(200);

                entity.HasOne(vv => vv.animal).WithMany().HasForeignKey(vv => vv.IdAnimal);
                entity.HasOne(vv => vv.foodWeightUnit).WithMany().HasForeignKey(vv => vv.IdWeightUnit).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<WeightUnit>(entity =>
            {
                entity.HasKey(wu => wu.Id);
                entity.Property(wu => wu.Name).HasMaxLength(20);
                entity.Property(wu => wu.Abbr).HasMaxLength(2);
            });

            modelBuilder.Entity<ZooService>(entity =>
            {
                entity.HasKey(zs => zs.Id);
                entity.Property(zs => zs.Name).HasMaxLength(100);
                entity.Property(zs => zs.Pic).HasMaxLength(150);
            });

            modelBuilder.Entity<EmployeeFeeding>(entity =>
            {
                entity.HasKey(ef => ef.Id);
                entity.HasOne(ef => ef.relatedAnimal).WithMany().HasForeignKey(ef => ef.IdAnimal).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(ef => ef.weightUnit).WithMany().HasForeignKey(ef => ef.IdWeightUnit).OnDelete(DeleteBehavior.NoAction);

            });
        }
    }
}
