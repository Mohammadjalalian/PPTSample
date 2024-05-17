using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SampleBackend.Data.Models;
using System;

namespace SampleBackend.Data
{
    public class SampleDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        public virtual DbSet<Image> Images { set; get; }

        public SampleDbContext(DbContextOptions<SampleDbContext> options,IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public SampleDbContext(DbContextOptions<SampleDbContext> options, string connectionString) : base(options)
        {
            _connectionString = connectionString;
        }

        //public SampleDbContext()
        //{
        //    var folder = Environment.SpecialFolder.LocalApplicationData;
        //    var path = Environment.GetFolderPath(folder);
        //    ConnectionString = $"Data Source={Path.Join(path, "data.db")}";
        //}

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (_configuration != null)
                    optionsBuilder.UseSqlite(_configuration.GetConnectionString("ConStr"));
                else
                {
                    optionsBuilder.UseSqlite(_connectionString);
                }
            }
        }


    }
}
