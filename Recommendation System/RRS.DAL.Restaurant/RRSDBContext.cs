using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RRS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRS.Core
{
    public class RRSDBContext : DbContext
    {
        public RRSDBContext() { }

        public virtual DbSet<RestaurantsVisited> RestaurantsVisited { get; set; }
        public virtual DbSet<CuisineTypes> CuisineTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configurationRoot = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
                string DBConStr = configurationRoot.GetConnectionString("MSSQLConStr");
                optionsBuilder.UseSqlServer(DBConStr, builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
