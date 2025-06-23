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

        //public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Company>().ToTable("Companies");
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
