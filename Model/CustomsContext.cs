using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


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
    public DbSet<Postleizahl> Postleizahlen => Set<Postleizahl>();

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
/// <summary>
/// Database model for countries
/// </summary>
public class Country
{

    /// <summary>
    /// AutoIncrement-ID of the config
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Country's A2 isocode
    /// </summary>
    public string A2Code { get; set; } //e.g. "DE", "PL"
    /// <summary>
    /// Is it a member of EUCU
    /// </summary>
    public bool isEUCU { get; set; }
    /// <summary>
    /// Concstructor
    /// </summary>
    /// <param name="A2Code">Country's A2 isocode</param>
    /// <param name="isEUCU">Is it a member of EUCU</param>
    public Country(string A2Code, bool isEUCU)
    {
        this.A2Code = A2Code;
        this.isEUCU = isEUCU;
    }
}


public class Postleizahl
{
    /// <summary>
    /// AutoIncrement-ID of the config
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Country's A2 isocode
    /// </summary>
    public string Country { get; set; } //e.g. "DE", "PL"
    /// <summary>
    /// Postal code 
    /// </summary>
    public string Code { get; set; }
    /// <summary>
    /// Type of the postal code: region or postal code
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// Costructor
    /// </summary>
    /// <param name="Country">Country's A2 isocode</param>
    /// <param name="Code">Postal code</param>
    /// <param name="Type">Type of the postal code: region or postal code</param>
    public Postleizahl(string Country, string Code, string Type)
    {
        this.Country = Country;
        this.Code = Code;
        this.Type = Type;
    }
}