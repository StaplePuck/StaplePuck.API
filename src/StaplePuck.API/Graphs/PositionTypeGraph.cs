using GraphQL.EntityFramework;
using StaplePuck.Core.Stats;
using StaplePuck.Data;

namespace StaplePuck.API.Graphs
{
    public class PositionTypeGraph : EfObjectGraphType<StaplePuckContext, PositionType>
    {
        public PositionTypeGraph(IEfGraphQLService<StaplePuckContext> graphQLService) : base(graphQLService)
        {
            Field(x => x.Id);
            Field(x => x.Name);
            AddNavigationListField(
                name: "scoringPositions",
                resolve: context => context.Source.ScoringPositions);
            Field(x => x.ShortName);
            AddNavigationField(
                name: "sport",
                resolve: context => context.Source.Sport);
        }
    }
}
