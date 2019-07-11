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
    using PokeAPI;

    public class EncounterResolver
    {
        public virtual async Task<EncounterMethod> GetEncounterMethodAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<EncounterMethod>(nameOrId).ConfigureAwait(false);

        public virtual async Task<EncounterCondition> GetEncounterConditionAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<EncounterCondition>(nameOrId).ConfigureAwait(false);

        public virtual async Task<EncounterConditionValue> GetEncounterConditionValueAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<EncounterConditionValue>(nameOrId).ConfigureAwait(false);
    }
}
