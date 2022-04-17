using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Options;

namespace StaplePuck.Data
{
    public class DbContextBuilder : IHostedService
    {
        public static StaplePuckContext BuildDBContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql(connectionString);
            return new StaplePuckContext(optionsBuilder.Options);
        }

        private readonly string _connectionString;
        private static StaplePuckContext _context;

        public DbContextBuilder(string connectionString)
        {
            _connectionString = connectionString;
        }

        public StaplePuckContext BuildDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql(_connectionString);
            return new StaplePuckContext(optionsBuilder.Options);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //var sqlInstance = new SqlInstance<SampleDbContext>(
            //buildTemplate: CreateDb,
            //constructInstance: builder => new SampleDbContext(builder.Options));
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql(_connectionString);
            _context = new StaplePuckContext(optionsBuilder.Options);
            
            //database = await sqlInstance.Build("GraphQLEntityFrameworkSample");
            await Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
