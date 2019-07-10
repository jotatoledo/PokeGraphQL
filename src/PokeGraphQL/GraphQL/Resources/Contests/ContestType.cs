﻿namespace PokeGraphQL.GraphQL.Resources.Contests
{
    using HotChocolate.Types;
    using PokeAPI;
    using PokeGraphQL.GraphQL.Resources.Berries;

    internal sealed class ContestTypeType : BaseApiObjectType<ContestType>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<ContestType> descriptor)
        {
            // TODO: create issue about overwritten class properties bounded implicitely
            descriptor.BindFields(BindingBehavior.Explicit);
            descriptor.Description("Contest types are categories judges used to weigh a pokémon's condition in pokemon contests.");
            descriptor.Field(x => x.Name)
                .Description($"The name for this {this.ResourceName} resource.")
                .Type<NonNullType<StringType>>();
            descriptor.Field(x => x.BerryFlavor)
                .Description("The berry flavor that correlates with this contest type.")
                .Type<BerryFlavorType>()
                .Resolver((ctx, token) => ctx.Service<BerryResolver>().GetBerryFlavorAsync(ctx.Parent<ContestType>().BerryFlavor.Name, token));
        }
    }
}