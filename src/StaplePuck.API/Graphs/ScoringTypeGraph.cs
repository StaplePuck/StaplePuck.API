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
