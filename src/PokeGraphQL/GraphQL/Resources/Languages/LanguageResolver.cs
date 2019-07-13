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
    using PokeAPI;

    public class LanguageResolver
    {
        public virtual async Task<Language> GetLanguageAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<Language>(nameOrId).ConfigureAwait(false);
    }
}
