using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;

namespace StaplePuck.API.Graphs
{
    public class ScoringTypeGraph : EfObjectGraphType<ScoringType>
    {
        public ScoringTypeGraph(IEfGraphQLService graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.Name);
            AddNavigationField<ScoringPositionsGraph, ScoringPositions>(
                name: "scoringPositions",
                resolve: context => context.Source.ScoringPositions);
            Field(x => x.ShortName);
            AddNavigationField<SportGraph, Sport>(
                name: "sport",
                resolve: context => context.Source.Sport);
        }
    }
}
