using GraphQL.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using StaplePuck.Data;

namespace StaplePuck.API
{
    /// <summary>
    /// The GraphQL user context for the current request. The user context is accessible in field resolvers and
    /// validation rules using <c>context.UserContext.As&lt;GraphQLUserContext&gt;()</c>.
    /// </summary>
    public class GraphQLUserContext : Dictionary<string, object>, IProvideClaimsPrincipal
    {

        /// <summary>
        /// Gets the current users claims principal.
        /// </summary>
        public ClaimsPrincipal User { get; set; }
    }
}
