using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using StaplePuck.Data;
using StaplePuck.Data.Repositories;
using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StaplePuck.Data.Tests.Repositories
{
    public class StatsRepositoryTests
    {
        [Fact]
        public void Test1()
        {
            var options = new DbContextOptionsBuilder<StaplePuckContext>()
                .UseInMemoryDatabase(databaseName: "DBCreation")
                .Options;

            using (var context = new StaplePuckContext(options))
            {
                //var repo = new StatsRepository(context);
                //repo.Add();
            }
            //var repo = new StatsRepository();
        }
    }
}
