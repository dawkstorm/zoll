using CustomsController.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Configuration for PostalCode database
/// </summary>
public class PostalCodeConfiguration : IEntityTypeConfiguration<PostalCode>
{
    /// <summary>
    /// Configure database
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<PostalCode> builder)
    {
        builder
            .Property(e => e.Type)
                  .HasConversion(
                      v => v.ToString(), // Convert enum to string
                      v => (PostalCodeType)Enum.Parse(typeof(PostalCodeType), v) // Convert string to enum
                  );
    }
}