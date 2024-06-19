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
    }
}

public class Country
{
    public string A2Code; //e.g. "DE", "PL"
    public bool isEU;
}
