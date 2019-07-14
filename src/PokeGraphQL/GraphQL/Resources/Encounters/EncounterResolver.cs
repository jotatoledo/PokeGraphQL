// <copyright file="EncounterResolver.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Encounters
{
    using System.Threading;
    using System.Threading.Tasks;
    using PokeApiNet.Data;
    using PokeApiNet.Models;

    public class EncounterResolver
    {
        private readonly PokeApiClient pokeApiClient;

        public EncounterResolver(PokeApiClient pokeApiClient)
        {
            this.pokeApiClient = pokeApiClient;
        }

        public virtual async Task<EncounterMethod> GetEncounterMethodAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<EncounterMethod>(nameOrId).ConfigureAwait(false);

        public virtual async Task<EncounterCondition> GetEncounterConditionAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<EncounterCondition>(nameOrId).ConfigureAwait(false);

        public virtual async Task<EncounterConditionValue> GetEncounterConditionValueAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<EncounterConditionValue>(nameOrId).ConfigureAwait(false);
    }
}
