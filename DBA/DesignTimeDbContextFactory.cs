﻿using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace DBA
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<clsDbContext>
    {
        public clsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<clsDbContext>();


            var configuration = new ConfigurationBuilder()
                .SetBasePath(@"C:\devlop\test\WebApplication1\bin\Release\net8.0")

                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ConnectionStr");

            optionsBuilder.UseSqlServer(connectionString);

            return new clsDbContext(optionsBuilder.Options);
        }
    }
}
