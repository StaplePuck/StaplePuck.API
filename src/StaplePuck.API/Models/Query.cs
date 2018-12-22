using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.EntityFramework;
using GraphQL.Types;
using StaplePuck.API.Graphs;
using StaplePuck.Core.Fantasy;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

namespace StaplePuck.API.Models
{
    public class Query : EfObjectGraphType
    {
        public Query(IEfGraphQLService efGraphQlService) : base(efGraphQlService)
        {
            //AddQueryField<FantasyTeamGraph, FantasyTeam>(
            //    name: "fantasyTeams",
            //    resolve: context =>
            //    {
            //        var dataContext = (StaplePuckContext)context.UserContext;
            //        return dataContext.FantasyTeams;
            //    });

            AddQueryField<ScoringTypeGraph, ScoringType>(
                name: "scoringTypes",
                resolve: context =>
                {
                    var dataContext = (StaplePuckContext)context.UserContext;
                    return dataContext.ScoringTypes;
                });
            //Addquer
        }
    }
}
