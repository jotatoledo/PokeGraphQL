// <copyright file="BerryResolver.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Berries
{
    using System.Threading;
    using System.Threading.Tasks;
    using PokeApiNet.Data;
    using PokeApiNet.Models;

    internal class BerryResolver
    {
        private readonly PokeApiClient pokeApiClient;

        public BerryResolver(PokeApiClient pokeApiClient)
        {
            this.pokeApiClient = pokeApiClient;
        }

        public virtual async Task<Berry> GetBerryAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<Berry>(nameOrId).ConfigureAwait(false);

        public virtual async Task<BerryFirmness> GetBerryFirmnessAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<BerryFirmness>(nameOrId).ConfigureAwait(false);

        public virtual async Task<BerryFlavor> GetBerryFlavorAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<BerryFlavor>(nameOrId).ConfigureAwait(false);
    }
}
