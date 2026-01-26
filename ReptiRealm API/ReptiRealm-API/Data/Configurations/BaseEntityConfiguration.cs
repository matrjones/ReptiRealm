using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReptiRealm_API.Entities.Common;

namespace ReptiRealm_API.Data.Configurations
{
    public abstract class BaseEntityConfiguration<T>
        : IEntityTypeConfiguration<T>
        where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.CreatedOn).IsRequired();
            builder.Property(e => e.CreatedBy).IsRequired();
        }
    }
}
