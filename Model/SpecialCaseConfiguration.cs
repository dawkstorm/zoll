using CustomsController.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Configuration for PostalCode database
/// </summary>
public class SpecialCaseConfiguration : IEntityTypeConfiguration<SpecialCase>
{
    /// <summary>
    /// Configure database
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<SpecialCase> builder)
    {
        builder
            .Property(e => e.Type)
                  .HasConversion(
                      v => v.ToString(), // Convert enum to string
                      v => (SpecialCaseType)Enum.Parse(typeof(SpecialCaseType), v) // Convert string to enum
                  );
    }
}