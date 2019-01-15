using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using StaplePuck.Core.Data;
using StaplePuck.Core.Stats;
using StaplePuck.API.Graphs;
using StaplePuck.API.Graphs.Input;

namespace StaplePuck.API.Models
{
    public class Mutation : ObjectGraphType
    {
        public Mutation(IStatsRepository statsRepository)
        {
            Name = "Mutation";

            Field<ResultGraph>(
                "createSeason",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SeasonInputType>> { Name = "season" }
                ),
                resolve: context =>
                {
                    var season = context.GetArgument<Season>("season");
                    return statsRepository.Add(season);
                });
        }
    }
}
