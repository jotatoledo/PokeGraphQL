namespace PokeGraphQL.GraphQL.Resources.Items
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class ItemFlingEffectType : BaseNamedApiObjectType<ItemFlingEffect>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<ItemFlingEffect> descriptor)
        {
            // TODO add missing props
            descriptor.Description("The various effects of the move \"Fling\" when used with different items.");
            descriptor.Ignore(x => x.Effects);
            descriptor.Field(x => x.Items)
                .Description("A list of items that have this fling effect.")
                .Type<ListType<ItemType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<ItemResolver>();
                    var resourceTasks = ctx.Parent<ItemFlingEffect>()
                        .Items
                        .Select(item => resolver.GetItemAsync(item.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
        }
    }
}
