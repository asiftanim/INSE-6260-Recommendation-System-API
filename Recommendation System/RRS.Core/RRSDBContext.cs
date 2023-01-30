using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RRS.Core.Entities;
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

        public virtual DbSet<RestaurantCuisin> RestaurantCuisin { get; set; }
        public virtual DbSet<RestaurantRating> RestaurantRating { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configurationRoot = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
                string DBConStr = configurationRoot.GetConnectionString("MSSQLConStr");
                optionsBuilder.UseSqlServer(DBConStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
