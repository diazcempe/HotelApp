using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HotelApp.Data.Contexts
{
    /// <summary>
    /// This class tells the tools how to create your DbContext by implementing the IDesignTimeDbContextFactory&lt;TContext&gt; interface:
    /// If a class implementing this interface is found in either the same project as the derived DbContext or in the application's startup project,
    /// the tools bypass the other ways of creating the DbContext and use the design-time factory instead.
    /// Reference:
    /// - https://blog.rodrigo-santos.me/mjalnXQC3wI0FvuLzxPF
    /// - https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation
    /// </summary>
    public class HotelAppDbContextFactory : IDesignTimeDbContextFactory<HotelAppDbContext>
    {
        public HotelAppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"{@Directory.GetCurrentDirectory()}/appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<HotelAppDbContext>()
                .UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));

            return new HotelAppDbContext(optionsBuilder.Options);
        }
    }
}
