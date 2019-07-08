namespace PokeGraphQL.GraphQL.Resources.Items
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class ItemAttributeType : BaseNamedApiObjectType<ItemAttribute>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<ItemAttribute> descriptor)
        {
            // TODO add missing fields
            descriptor.Description("Item attributes define particular aspects of items, e.g. \"usable in battle\" or \"consumable\".");
            descriptor.Ignore(x => x.Descriptions);
            descriptor.Field(x => x.Items)
                .Description("A list of items that have this attribute.")
                .Type<ListType<ItemType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<ItemResolver>();
                    var resourceTasks = ctx.Parent<ItemAttribute>().Items.Select(item => resolver.GetItemAsync(item.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
        }
    }
}
