// <copyright file="ItemAttributeType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Items
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal sealed class ItemAttributeType : BaseNamedApiObjectType<ItemAttribute>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<ItemAttribute> descriptor)
        {
            descriptor.Description("Item attributes define particular aspects of items, e.g. \"usable in battle\" or \"consumable\".");
            descriptor.UseNamedApiResourceCollectionField<ItemAttribute, Item, ItemType>(x => x.Items);

            // TODO add missing field
            descriptor.Ignore(x => x.Descriptions);
        }
    }
}
