using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ok.Tech.Domain.Entities;

namespace Ok.Tech.Infra.Data.Mappongs
{
    public class PriceListMapping : IEntityTypeConfiguration<PriceList>
    {
        public void Configure(EntityTypeBuilder<PriceList> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(200).HasColumnType("VARCHAR(200)");

            builder.Property(p => p.Active).IsRequired();

            builder.HasMany(p => p.Prices).WithOne(p => p.PriceList).HasForeignKey(f => f.PriceListId);

            builder.ToTable("PriceLists");

        }
    }
}