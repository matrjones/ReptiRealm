using ReptiRealm.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReptiRealm.Authentication;

namespace ReptiRealm.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Defecation> Defecations { get; set; }
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<FoodType> FoodTypes { get; set; }
        public DbSet<Morph> Morphs { get; set; }
        public DbSet<Regurgitation> Regurgitations { get; set; }
        public DbSet<Reptile> Reptiles { get; set; }
        public DbSet<Shed> Sheds { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Weight> Weights { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Defecation>().ToTable("Defecations");
            modelBuilder.Entity<Feed>().ToTable("Feeds");
            modelBuilder.Entity<Morph>().ToTable("Morphs");
            modelBuilder.Entity<FoodType>().ToTable("FoodTypes");
            modelBuilder.Entity<Regurgitation>().ToTable("Regurgitations");
            modelBuilder.Entity<Reptile>().ToTable("Reptiles");
            modelBuilder.Entity<Shed>().ToTable("Sheds");
            modelBuilder.Entity<Species>().ToTable("Species");
            modelBuilder.Entity<Weight>().ToTable("Weight");
            modelBuilder.Entity<Subscription>().ToTable("Subscriptions");
        }

        public override int SaveChanges()
        {
            var added = ChangeTracker.Entries<IBaseEntity>().Where(E => E.State == EntityState.Added).ToList();

            added.ForEach(E =>
            {
                E.Property(x => x.CreatedDate).CurrentValue = DateTime.UtcNow;
                E.Property(x => x.CreatedDate).IsModified = true;
                E.Property(x => x.ModifiedDate).CurrentValue = DateTime.UtcNow;
                E.Property(x => x.ModifiedDate).IsModified = true;
            });

            var modified = ChangeTracker.Entries<IBaseEntity>().Where(E => E.State == EntityState.Modified).ToList();

            modified.ForEach(E =>
            {
                E.Property(x => x.ModifiedDate).CurrentValue = DateTime.UtcNow;
                E.Property(x => x.ModifiedDate).IsModified = true;

                E.Property(x => x.CreatedDate).CurrentValue = E.Property(x => x.CreatedDate).OriginalValue;
                E.Property(x => x.CreatedDate).IsModified = false;
            });

            return base.SaveChanges();
        }
    }
}
