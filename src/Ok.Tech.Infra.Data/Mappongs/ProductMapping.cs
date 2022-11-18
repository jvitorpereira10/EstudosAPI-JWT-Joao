using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ok.Tech.Domain.Entities;

namespace Ok.Tech.Infra.Data.Mappongs
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);

            builder.Property(p => p.Description).IsRequired().HasMaxLength(1000);

            builder.Property(p => p.Active).IsRequired();

            builder.HasMany(p => p.Prices).WithOne(p => p.Product).HasForeignKey(f => f.ProductId);

            builder.ToTable("Products");
        }
    }
}
