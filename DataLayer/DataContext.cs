using BusinessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class DataContext : DbContext
    {
        private string connectionString;

        public DataContext(string db = "production") : base()
        {
            SetConnectionString(db);
        }

        private void SetConnectionString(string db = "production")
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            switch (db.ToLower())
            {
                case "production":
                    this.connectionString = configuration.GetConnectionString("Production").ToString();
                    break;
                case "development":
                    this.connectionString = configuration.GetConnectionString("Development").ToString();
                    break;
            }
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Country> Counties { get; set; }
        public DbSet<River> Rivers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (this.connectionString == null)
                this.SetConnectionString();
            optionsBuilder.UseSqlServer(this.connectionString);
        }
    }
}
