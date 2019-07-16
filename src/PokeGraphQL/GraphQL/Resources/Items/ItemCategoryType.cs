// <copyright file="ItemCategoryType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Items
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal sealed class ItemCategoryType : BaseNamedApiObjectType<ItemCategory>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<ItemCategory> descriptor)
        {
            descriptor.Description("Item categories determine where items will be placed in the players bag.");
            descriptor.UseNamedApiResourceCollectionField<ItemCategory, Item, ItemType>(x => x.Items);
            descriptor.UseNamedApiResourceField<ItemCategory, ItemPocket, ItemPocketType>(x => x.Pocket);
        }
    }
}
