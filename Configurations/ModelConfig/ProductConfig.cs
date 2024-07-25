using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestDtoInApi.Models;

namespace TestDtoInApi.Configurations.ModelConfig
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(e => e.ProductId);
            builder.Property(x => x.ProductId).UseIdentityColumn();

            builder.Property(x => x.ProductName).IsRequired();
            builder.Property(x => x.ProductName).HasMaxLength(250);

            builder.Property(x => x.ProductPrice).IsRequired();
            builder.Property(x => x.ProductPrice).HasColumnType("Decimal(7, 2)");

            builder.Property(x => x.InStock).IsRequired();

            builder.HasOne(x => x.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.CategoryId)
                .HasConstraintName("FK_Product_Category");
        }
    }
}
