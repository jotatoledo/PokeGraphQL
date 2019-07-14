// <copyright file="MachineResolver.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Machines
{
    using System.Threading;
    using System.Threading.Tasks;
    using PokeApiNet.Data;
    using PokeApiNet.Models;

    public class MachineResolver
    {
        private readonly PokeApiClient pokeApiClient;

        public MachineResolver(PokeApiClient pokeApiClient)
        {
            this.pokeApiClient = pokeApiClient;
        }

        public virtual async Task<Machine> GetMachineAsync(int id, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceAsync<Machine>(id).ConfigureAwait(false);
    }
}
