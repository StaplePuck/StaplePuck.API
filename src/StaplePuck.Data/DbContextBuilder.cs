using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace StaplePuck.Data
{
    public static class DbContextBuilder
    {
        public static StaplePuckContext BuildDBContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql(connectionString);
            return new StaplePuckContext(optionsBuilder.Options);
        }
    }
}
