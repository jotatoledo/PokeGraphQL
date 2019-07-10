﻿namespace PokeGraphQL.GraphQL.Resources.Items
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class ItemPocketType : BaseNamedApiObjectType<ItemPocket>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<ItemPocket> descriptor)
        {
            descriptor.Description("Pockets within the players bag used for storing items by category.");
            descriptor.Field(x => x.Categories)
                .Description("A list of item categories that are relevent to this item pocket.")
                .Type<ListType<ItemCategoryType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<ItemResolver>();
                    var resourceTasks = ctx.Parent<ItemPocket>().Categories.Select(category => resolver.GetCategoryAsync(category.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
        }
    }
}