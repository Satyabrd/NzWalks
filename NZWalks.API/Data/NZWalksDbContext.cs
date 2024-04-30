using System;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace NZWalks.API.Data
{
	public class NZWalksDbContext: DbContext
    {
        /*public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{
		}*/
        private readonly IConfiguration _configuration;

        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions, IConfiguration configuration)
            : base(dbContextOptions)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("NZWalksConnectionString");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<Difficulty> Difficulties { get; set; }
		public DbSet<Region> Regions { get; set; }
		public DbSet<Walk> Walks { get; set; }
	}
}

