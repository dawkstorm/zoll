using Microsoft.EntityFrameworkCore;


/// <summary>
/// Database model for Customs
/// </summary>
public class CustomsContext : DbContext
{
    /// <summary>
    /// Countries database
    /// </summary>
    public DbSet<Country> Countries => Set<Country>();

    /// <summary>
    /// Postcodes database
    /// </summary>
    public DbSet<PostalCode> Postleizahlen => Set<PostalCode>();

    /// <summary>
    /// Path to the database
    /// </summary>
    public string DbPath { get; }

    public CustomsContext(DbContextOptions<CustomsContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
    }
}



