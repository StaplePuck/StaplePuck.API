using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace StaplePuck.Data
{
    public class TemporaryDbContextFactory : DesignTimeDbContextFactoryBase<StaplePuckContext>
    {
        protected override StaplePuckContext CreateNewInstance(
            DbContextOptions<StaplePuckContext> options)
        {
            return new StaplePuckContext(options);
        }
    }
}
