namespace PokeGraphQL.GraphQL.Resources.Berries
{
    using HotChocolate.Types;
    using PokeAPI;
    using PokeGraphQL.GraphQL.Resources.Contests;

    internal sealed class BerryFlavorType : BaseNamedApiObjectType<BerryFlavor>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<BerryFlavor> descriptor)
        {
            descriptor.Description("Flavors determine whether a Pokémon will benefit or suffer from eating a berry based on their nature.");
            descriptor.Field(x => x.ContestType)
                .Type<ContestTypeType>()
                .Resolver((ctx, token) => ctx.Service<ContestResolver>().GetContestTypeAsync(ctx.Parent<BerryFlavor>().ContestType.Name, token));
            descriptor.Field(x => x.Berries)
                .Type<ListType<FlavorBerryMapType>>();
        }

        private sealed class FlavorBerryMapType : ObjectType<FlavorBerryMap>
        {
            /// <inheritdoc/>
            protected override void Configure(IObjectTypeDescriptor<FlavorBerryMap> descriptor)
            {
                // TODO remove once hotchocolate@9.1.0 lands
                descriptor.BindFields(BindingBehavior.Explicit);
                descriptor.Field(x => x.Potency);
                descriptor.Field(x => x.Berry)
                    .Description("The berry with the referenced flavor.")
                    .Type<BerryType>()
                    .Resolver((ctx, token) => ctx.Service<BerryResolver>().GetBerryAsync(ctx.Parent<FlavorBerryMap>().Berry.Name, token));
            }
        }
    }
}
