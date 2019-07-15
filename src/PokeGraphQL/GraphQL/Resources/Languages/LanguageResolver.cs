// <copyright file="LanguageResolver.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Languages
{
    using System.Threading;
    using System.Threading.Tasks;
    using PokeApiNet.Data;
    using PokeApiNet.Models;

    public class LanguageResolver
    {
        private readonly PokeApiClient pokeApiClient;

        public LanguageResolver(PokeApiClient pokeApiClient)
        {
            this.pokeApiClient = pokeApiClient;
        }

        public virtual async Task<Language> GetLanguageAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<Language>(nameOrId).ConfigureAwait(false);
    }
}
