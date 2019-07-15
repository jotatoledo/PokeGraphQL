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
    using PokeApiNet.Data;
    using PokeApiNet.Models;

    public class EvolutionResolver
    {
        private readonly PokeApiClient pokeApiClient;

        public EvolutionResolver(PokeApiClient pokeApiClient)
        {
            this.pokeApiClient = pokeApiClient;
        }

        public virtual async Task<EvolutionChain> GetEvolutionChainAsync(int id, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceAsync<EvolutionChain>(id).ConfigureAwait(false);

        public virtual async Task<EvolutionTrigger> GetEvolutionTriggerAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<EvolutionTrigger>(nameOrId).ConfigureAwait(false);
    }
}
