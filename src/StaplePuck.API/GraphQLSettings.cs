using GraphQL.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaplePuck.API
{
    public class GraphQLSettings
    {
        public Func<HttpContext, Task<object>> BuildUserContext { get; set; }
        public object Root { get; set; }
        public List<IValidationRule> ValidationRules { get; } = new List<IValidationRule>();
    }
}
