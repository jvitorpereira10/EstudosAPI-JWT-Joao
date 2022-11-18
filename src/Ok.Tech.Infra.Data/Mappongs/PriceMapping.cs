using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ok.Tech.Domain.Entities;

namespace Ok.Tech.Infra.Data.Mappongs
{
    public class PriceMapping : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Value).IsRequired().HasColumnType("DECIMAL(10,2)");

            builder.HasOne(p => p.PriceList).WithMany(p => p.Prices);

            builder.HasOne(p => p.Product).WithMany(p => p.Prices);

            builder.ToTable("Prices");
        }
    }
}