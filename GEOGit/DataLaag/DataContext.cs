using BusinessLaag.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace DataLaag
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-2RD68A3P\SQLEXPRESS01;Initial Catalog=GEOLander;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasOne(x => x.Continent);
        }

        public DbSet<Country> DbCountry { get; set; }
        public DbSet<Continent> DbContinent { get; set; }
        public DbSet<City> DbCity { get; set; }
    }
}