// <copyright file="ItemFlingEffectType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Items
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Common;

    internal sealed class ItemFlingEffectType : BaseNamedApiObjectType<ItemFlingEffect>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<ItemFlingEffect> descriptor)
        {
            descriptor.Description("The various effects of the move \"Fling\" when used with different items.");
            descriptor.Field(x => x.EffectEntries)
                .Description("The result of this fling effect listed in different languages.")
                .Type<ListType<EffectsType>>();
            descriptor.Field(x => x.Items)
                .Description("A list of items that have this fling effect.")
                .Type<ListType<ItemType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<ItemResolver>();
                    var resourceTasks = ctx.Parent<ItemFlingEffect>()
                        .Items
                        .Select(item => resolver.GetItemAsync(item.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
        }
    }
}
