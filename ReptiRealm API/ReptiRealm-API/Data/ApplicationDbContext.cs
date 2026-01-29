using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReptiRealm_API.Entities;
using ReptiRealm_API.Entities.Common;

namespace ReptiRealm_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
        
        public DbSet<Reptile> Reptiles => Set<Reptile>();
        public DbSet<Species> Species => Set<Species>();
        public DbSet<Morph> Morphs => Set<Morph>();
        public DbSet<Feed> Feeds => Set<Feed>();
        public DbSet<FoodType> FoodTypes => Set<FoodType>();
        public DbSet<Regurgitation> Regurgitations => Set<Regurgitation>();
        public DbSet<Shed> Sheds => Set<Shed>();
        public DbSet<Weight> Weights => Set<Weight>();
        public DbSet<Defecation> Defecations => Set<Defecation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // User -> Reptile (Cascade)
            modelBuilder.Entity<Reptile>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reptiles)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User -> Species (Restrict)
            modelBuilder.Entity<Species>()
                .HasOne(s => s.User)
                .WithMany(u => u.Species)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reptile -> Feeds (Cascade)
            modelBuilder.Entity<Feed>()
                .HasOne(f => f.Reptile)
                .WithMany(r => r.Feeds)
                .HasForeignKey(f => f.ReptileId)
                .OnDelete(DeleteBehavior.Cascade);

            // Reptile -> Sheds (Cascade)
            modelBuilder.Entity<Shed>()
                .HasOne(s => s.Reptile)
                .WithMany(r => r.Sheds)
                .HasForeignKey(s => s.ReptileId)
                .OnDelete(DeleteBehavior.Cascade);

            // Reptile -> Weights (Cascade)
            modelBuilder.Entity<Weight>()
                .HasOne(w => w.Reptile)
                .WithMany(r => r.Weights)
                .HasForeignKey(w => w.ReptileId)
                .OnDelete(DeleteBehavior.Cascade);

            // Reptile -> Defecations (Cascade)
            modelBuilder.Entity<Defecation>()
                .HasOne(d => d.Reptile)
                .WithMany(r => r.Defecations)
                .HasForeignKey(d => d.ReptileId)
                .OnDelete(DeleteBehavior.Cascade);

            // Species -> Morph (Cascade)
            modelBuilder.Entity<Morph>()
                .HasOne(m => m.Species)
                .WithMany(s => s.Morphs)
                .HasForeignKey(m => m.SpeciesId)
                .OnDelete(DeleteBehavior.Cascade);

            // Species -> Reptiles (Set Null)
            modelBuilder.Entity<Reptile>()
                .HasOne(r => r.Species)
                .WithMany(s => s.Reptiles)
                .HasForeignKey(r => r.SpeciesId)
                .OnDelete(DeleteBehavior.SetNull);

            // Weight Precision
            modelBuilder.Entity<Weight>()
                .Property(w => w.Value)
                .HasPrecision(7, 3);

            // Reptile <-> Morph (Many-to-Many)
            modelBuilder.Entity<Reptile>()
                .HasMany(r => r.Morphs)
                .WithMany(m => m.Reptiles);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
