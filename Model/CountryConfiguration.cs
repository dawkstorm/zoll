using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder
            .HasKey(config => config.Id);
        builder
            .Property(c => c.A2Code)
            .HasMaxLength(2);
    }
}