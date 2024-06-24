using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
/// <summary>
/// Configuration for Countries database
/// </summary>
public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    /// <summary>
    /// Configure database
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder
            .HasKey(config => config.Id);
        builder
            .Property(c => c.A2Code)
            .HasMaxLength(2);
    }
}