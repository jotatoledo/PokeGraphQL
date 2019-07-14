// <copyright file="ItemResolver.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Items
{
    using System.Threading;
    using System.Threading.Tasks;
    using PokeApiNet.Data;
    using PokeApiNet.Models;

    public class ItemResolver
    {
        private readonly PokeApiClient pokeApiClient;

        public ItemResolver(PokeApiClient pokeApiClient)
        {
            this.pokeApiClient = pokeApiClient;
        }

        public virtual async Task<Item> GetItemAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<Item>(nameOrId).ConfigureAwait(false);

        public virtual async Task<ItemAttribute> GetAttributeAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<ItemAttribute>(nameOrId).ConfigureAwait(false);

        public virtual async Task<ItemCategory> GetCategoryAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<ItemCategory>(nameOrId).ConfigureAwait(false);

        public virtual async Task<ItemPocket> GetPocketAsync(string nameOrId, CancellationToken token = default) => await this.pokeApiClient.GetResourceFromParamAsync<ItemPocket>(nameOrId).ConfigureAwait(false);

        public virtual async Task<ItemFlingEffect> GetFlingEffectAsync(string nameOrId, CancellationToken token = default) => await this.pokeApiClient.GetResourceFromParamAsync<ItemFlingEffect>(nameOrId).ConfigureAwait(false);
    }
}