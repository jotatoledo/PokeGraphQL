// <copyright file="ItemFlingEffectType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Items
{
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
            descriptor.UseNamedApiResourceCollectionField<ItemFlingEffect, Item, ItemType>(x => x.Items);
        }
    }
}
