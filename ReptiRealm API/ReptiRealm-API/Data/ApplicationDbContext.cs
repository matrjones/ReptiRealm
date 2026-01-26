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
            
            modelBuilder.Entity<Reptile>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reptiles)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Weight>()
                .Property(w => w.Value)
                .HasPrecision(5, 3);

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
