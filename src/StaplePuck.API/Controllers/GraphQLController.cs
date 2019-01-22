using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;

namespace StaplePuck.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class GraphQLController : ControllerBase
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;

        public GraphQLController(ISchema schema, IDocumentExecuter documentExecuter)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
        }

        [HttpPost]
        public Task<ExecutionResult> Post(
                [BindRequired, FromBody] PostBody body,
                [FromServices] StaplePuck.Data.StaplePuckContext dataContext,
                CancellationToken cancellation)
        {
            return Execute(dataContext, body.Query, body.OperationName, body.Variables, cancellation);
        }

        public class PostBody
        {
            public string OperationName;
            public string Query;
            public JObject Variables;
        }

        [HttpGet]
        public Task<ExecutionResult> Get(
            [FromQuery] string query,
            [FromQuery] string variables,
            [FromQuery] string operationName,
            [FromServices] StaplePuck.Data.StaplePuckContext dataContext,
            CancellationToken cancellation)
        {
            var jObject = ParseVariables(variables);
            return Execute(dataContext, query, operationName, jObject, cancellation);
        }

        async Task<ExecutionResult> Execute(StaplePuck.Data.StaplePuckContext dataContext, string query, string operationName, JObject variables, CancellationToken cancellation)
        {
            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query,
                OperationName = operationName,
                Inputs = variables?.ToInputs(),
                UserContext = dataContext,
                CancellationToken = cancellation,
#if (DEBUG)
                ExposeExceptions = true,
                EnableMetrics = true,
#endif
            };

            var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return result;
        }

        static JObject ParseVariables(string variables)
        {
            if (variables == null)
            {
                return null;
            }

            try
            {
                return JObject.Parse(variables);
            }
            catch (Exception exception)
            {
                throw new Exception("Could not parse variables.", exception);
            }
        }
    }
}
