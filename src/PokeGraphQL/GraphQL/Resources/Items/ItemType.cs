namespace PokeGraphQL.GraphQL.Resources.Items
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;
    using PokeGraphQL.GraphQL.Resources.Games;

    internal sealed class ItemType : BaseNamedApiObjectType<Item>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Item> descriptor)
        {
            // TODO implement ignored fields
            // TODO add missing props: effect_entries, flavor_text_entries, baby_trigger_for
            // TODO remove next
            descriptor.BindFields(BindingBehavior.Explicit);
            descriptor.Description(@"An item is an object in the games which the player can pick up, keep in their bag, and use in some manner. 
                They have various uses, including healing, powering up, helping catch Pokémon, or to access a new area.");
            descriptor.Field(x => x.Cost)
                .Description("The price of this item in stores.");
            descriptor.Field(x => x.FlingPower)
                .Description("The power of the move Fling when used with this item.");
            descriptor.Field(x => x.FlingEffect)
                .Description("The effect of the move Fling when used with this item.")
                .Type<ItemFlingEffectType>()
                .Resolver((ctx, token) => ctx.Service<ItemResolver>().GetFlingEffectAsync(ctx.Parent<Item>().FlingEffect.Name, token));
            descriptor.Field(x => x.Attributes)
                .Description("A list of attributes this item has.")
                .Type<ListType<ItemAttributeType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<ItemResolver>();
                    var resourceTasks = ctx.Parent<Item>().Attributes.Select(attribute => resolver.GetAttributeAsync(attribute.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
            descriptor.Field(x => x.Category)
                .Description("The category of items this item falls into.")
                .Type<ItemCategoryType>()
                .Resolver((ctx, token) => ctx.Service<ItemResolver>().GetCategoryAsync(ctx.Parent<Item>().Category.Name, token));
            descriptor.Field(x => x.HeldBy)
                .Description("A list of pokémon that might be found in the wild holding this item.")
                .Type<ListType<ItemHeldByType>>();
            descriptor.Field(x => x.GameIndices)
                .Description("A list of game indices relevent to this item by generation.")
                .Ignore();
        }

        internal sealed class ItemHeldByType : ObjectType<ItemHeldBy>
        {
            /// <inheritdoc/>
            protected override void Configure(IObjectTypeDescriptor<ItemHeldBy> descriptor)
            {
                // TODO: implement ignored fields
                descriptor.Field(x => x.Pokemon)
                    .Description("The pokemon who might be holding the item.")
                    .Ignore();
                descriptor.Field(x => x.VersionDetails)
                    .Description("Details on chance of the pokemon having the item based on version.")
                    .Type<ListType<VersionDetailsType>>();
            }
        }

        internal sealed class VersionDetailsType : ObjectType<VersionDetails>
        {
            /// <inheritdoc/>
            protected override void Configure(IObjectTypeDescriptor<VersionDetails> descriptor)
            {
                descriptor.Field(x => x.Rarity)
                    .Description("The chance of the pokemon holding the item.");
                descriptor.Field(x => x.Version)
                    .Description("The version the rarity applies.")
                    .Type<VersionType>()
                    .Resolver((ctx, token) => ctx.Service<GameResolver>().GetVersionAsync(ctx.Parent<VersionDetails>().Version.Name, token));
            }
        }
    }
}
