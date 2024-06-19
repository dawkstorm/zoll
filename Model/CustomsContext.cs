using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class CustomsContext : DbContext
{
    public DbSet<Country> Countries { get; set; }
    public string DbPath { get; }

    public CustomsContext(DbContextOptions<CustomsContext> options) : base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "countries.db");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Country>().HasData();
    }
}

public class Country
{
    public string A2Code; //e.g. "DE", "PL"
    public bool isEU;
}
