// <copyright file="GameResolver.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Games
{
    using System.Threading;
    using System.Threading.Tasks;
    using PokeApiNet.Data;
    using PokeApiNet.Models;

    public class GameResolver
    {
        private readonly PokeApiClient pokeApiClient;

        public GameResolver(PokeApiClient pokeApiClient)
        {
            this.pokeApiClient = pokeApiClient;
        }

        public virtual async Task<Generation> GetGenerationAsync(string nameOrId, CancellationToken token = default) => await this.pokeApiClient.GetResourceFromParamAsync<Generation>(nameOrId).ConfigureAwait(false);

        public virtual async Task<Version> GetVersionAsync(string nameOrId, CancellationToken token = default) => await this.pokeApiClient.GetResourceFromParamAsync<Version>(nameOrId).ConfigureAwait(false);

        public virtual async Task<VersionGroup> GetVersionGroupAsync(string nameOrId, CancellationToken token = default) => await this.pokeApiClient.GetResourceFromParamAsync<VersionGroup>(nameOrId).ConfigureAwait(false);

        public virtual async Task<Pokedex> GetPokedexAsync(string nameOrId, CancellationToken token = default) => await this.pokeApiClient.GetResourceFromParamAsync<Pokedex>(nameOrId).ConfigureAwait(false);
    }
}
