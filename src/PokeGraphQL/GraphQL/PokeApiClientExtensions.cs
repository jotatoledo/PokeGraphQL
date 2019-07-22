// <copyright file="PokeApiClientExtensions.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL
{
    using System.Threading;
    using System.Threading.Tasks;
    using PokeApiNet.Data;
    using PokeApiNet.Models;

    internal static class PokeApiClientExtensions
    {
        internal static Task<TResource> GetResourceFromParamAsync<TResource>(this PokeApiClient client, string nameOrId, CancellationToken cancellationToken = default)
            where TResource : ResourceBase =>
        int.TryParse(nameOrId, out var id)
                ? client.GetResourceAsync<TResource>(id, cancellationToken)
                : client.GetResourceAsync<TResource>(nameOrId, cancellationToken);
    }
}
