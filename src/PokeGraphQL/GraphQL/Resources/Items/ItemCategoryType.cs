// <copyright file="ItemCategoryType.cs" company="PokeGraphQL.Net">
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
    using PokeAPI;

    internal sealed class ItemCategoryType : BaseNamedApiObjectType<ItemCategory>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<ItemCategory> descriptor)
        {
            descriptor.Description("Item categories determine where items will be placed in the players bag.");
            descriptor.Field(x => x.Items)
                .Description("A list of items that are a part of this category.")
                .Type<ListType<ItemType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<ItemResolver>();
                    var resourceTasks = ctx.Parent<ItemCategory>().Items.Select(item => resolver.GetItemAsync(item.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
            descriptor.Field(x => x.Pocket)
                .Description("The pocket items in this category would be put in.")
                .Type<ItemPocketType>()
                .Resolver((ctx, token) => ctx.Service<ItemResolver>().GetPocketAsync(ctx.Parent<ItemCategory>().Pocket.Name, token));
        }
    }
}
