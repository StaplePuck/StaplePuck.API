﻿using StaplePuck.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaplePuck.Core.Client
{
    /// <summary>
    /// Provides an interface for interacting with the StaplePuck API.
    /// </summary>
    public interface IStaplePuckClient
    {
        /// <summary>
        /// Updates an object.
        /// </summary>
        /// <typeparam name="T">The type of object to update.</typeparam>
        /// <param name="mutationName">The name of the mutation.</param>
        /// <param name="value">The value to update.</param>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="typeName">The name of the type for the variable.</param>
        /// <returns>The resulting message.</returns>
        Task<ResultModel> UpdateAsync<T>(string mutationName, T value, string variableName = null, string typeName = null);

        Task<T[]> GetAsync<T>(string query, IDictionary<string, object> variables = null);
    }
}
