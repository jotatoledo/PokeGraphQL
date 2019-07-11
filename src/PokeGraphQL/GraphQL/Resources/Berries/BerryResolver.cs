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
    using PokeAPI;

    internal class BerryResolver
    {
        public virtual async Task<Berry> GetBerryAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<Berry>(nameOrId).ConfigureAwait(false);

        public virtual async Task<BerryFirmness> GetBerryFirmnessAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<BerryFirmness>(nameOrId).ConfigureAwait(false);

        public virtual async Task<BerryFlavor> GetBerryFlavorAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<BerryFlavor>(nameOrId).ConfigureAwait(false);
    }
}
