// <copyright file="LocationResolver.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Locations
{
    using System.Threading;
    using System.Threading.Tasks;
    using PokeApiNet.Data;
    using PokeApiNet.Models;

    public class LocationResolver
    {
        private readonly PokeApiClient pokeApiClient;

        public LocationResolver(PokeApiClient pokeApiClient)
        {
            this.pokeApiClient = pokeApiClient;
        }

        public virtual async Task<Location> GetLocationAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<Location>(nameOrId).ConfigureAwait(false);

        public virtual async Task<LocationArea> GetLocationAreaAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<LocationArea>(nameOrId).ConfigureAwait(false);

        public virtual async Task<PalParkArea> GetPalParkAreaAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<PalParkArea>(nameOrId).ConfigureAwait(false);

        public virtual async Task<Region> GetRegionAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<Region>(nameOrId).ConfigureAwait(false);
    }
}
