// <copyright file="EvolutionResolver.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Evolutions
{
    using System.Threading;
    using System.Threading.Tasks;
    using PokeAPI;

    public class EvolutionResolver
    {
        public virtual async Task<EvolutionChain> GetEvolutionChainAsync(int id, CancellationToken cancellationToken = default) => await DataFetcher.GetApiObject<EvolutionChain>(id).ConfigureAwait(false);

        public virtual async Task<EvolutionTrigger> GetEvolutionTriggerAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<EvolutionTrigger>(nameOrId).ConfigureAwait(false);
    }
}
