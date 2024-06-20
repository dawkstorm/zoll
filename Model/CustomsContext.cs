using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class CustomsContext : DbContext
{
    public DbSet<Country> Countries => Set<Country>();

    public string DbPath { get; }

    public CustomsContext(DbContextOptions<CustomsContext> options) : base(options)
    {
       
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder){
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
    }
}

public class Country
{
    
/// <summary>
    /// AutoIncrement-ID of the config
    /// </summary>
    public int Id { get; set; }
    public string A2Code {get; set;} //e.g. "DE", "PL"
    public bool isEUCU {get; set;}

    public Country(string A2Code, bool isEUCU){
        this.A2Code = A2Code;
        this.isEUCU = isEUCU;
    }
}
