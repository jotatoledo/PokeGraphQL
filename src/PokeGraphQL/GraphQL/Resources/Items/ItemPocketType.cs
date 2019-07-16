// <copyright file="ItemPocketType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Items
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal sealed class ItemPocketType : BaseNamedApiObjectType<ItemPocket>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<ItemPocket> descriptor)
        {
            descriptor.Description("Pockets within the players bag used for storing items by category.");
            descriptor.UseNamedApiResourceCollectionField<ItemPocket, ItemCategory, ItemCategoryType>(x => x.Categories);
        }
    }
}
