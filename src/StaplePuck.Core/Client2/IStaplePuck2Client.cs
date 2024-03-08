using GraphQL;
using System.Threading;
using System.Threading.Tasks;

namespace StaplePuck.Core.Client2
{
    public interface IStaplePuck2Client
    {
        Task<T?> ExecuteMutationAsync<T>(GraphQLRequest request, string mutationName, CancellationToken cancellationToken) where T : class;
        Task<T> ExecuteQuery<T>(GraphQLRequest request, string queryName, CancellationToken cancellationToken);
    }
}