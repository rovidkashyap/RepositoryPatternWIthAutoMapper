using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestDtoInApi.Models;

namespace TestDtoInApi.Configurations.ModelConfig
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(x => x.CategoryId);

            builder.Property(x => x.CategoryId).UseIdentityColumn();

            builder.Property(x => x.CategoryName).IsRequired();
            builder.Property(x => x.CategoryName).HasMaxLength(250);
        }
    }
}
