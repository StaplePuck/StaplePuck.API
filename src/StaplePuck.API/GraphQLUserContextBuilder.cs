﻿using GraphQL.Server.Transports.AspNetCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using StaplePuck.Data;
using System.Text;
using System.Collections.Generic;

namespace StaplePuck.API
{
    //public class GraphQLUserContextBuilder : IUserContextBuilder
    //{
    //    private StaplePuckContext _dateContext;

    //    public GraphQLUserContextBuilder(StaplePuckContext dataContext)
    //    {
    //        _dateContext = dataContext;
    //    }
    //    public Task<object> BuildUserContext(HttpContext httpContext)
    //    {
    //        return Task.FromResult<object>(new GraphQLUserContext() { User = httpContext.User, DataContext = _dateContext });
    //    }

    //    Task<IDictionary<string, object>> IUserContextBuilder.BuildUserContext(HttpContext httpContext)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}
}
