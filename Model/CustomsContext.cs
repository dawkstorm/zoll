using Microsoft.EntityFrameworkCore;

/// <summary>
/// Database model for Customs
/// </summary>
public class CustomsContext : DbContext
{
    public CustomsContext(DbContextOptions<CustomsContext> options) : base(options)
    {

    }
    /// <summary>
    /// Countries database
    /// </summary>
    public DbSet<Country> Countries => Set<Country>();

    /// <summary>
    /// Postcodes database
    /// </summary>
    public DbSet<PostalCode> PostalCodes => Set<PostalCode>();

    /// <summary>
    /// Path to the database
    /// </summary>
    public string DbPath { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CountryConfiguration());

        modelBuilder.Entity<Country>()
            .HasMany(e => e.PostalCodes)
            .WithOne(e => e.Country)
            .HasForeignKey(e => e.CountryID)
            .HasPrincipalKey(e => e.Id);
    }
}