// <copyright file="BerryType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Berries
{
    using HotChocolate.Types;
    using PokeAPI;
    using PokeGraphQL.GraphQL.Resources.Items;
    using PokeGraphQL.GraphQL.Resources.Pokemons;

    internal sealed class BerryType : BaseNamedApiObjectType<Berry>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Berry> descriptor)
        {
            descriptor.Description("Berries are small fruits that can provide HP and status condition restoration, stat enhancement, and even damage negation when eaten by pokemon.");
            descriptor.Field(x => x.Flavors)
                .Type<ListType<BerryFlavorMapType>>();
            descriptor.Field(x => x.NaturalGiftType)
                .Type<TypePropertyType>()
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetTypeAsync(ctx.Parent<Berry>().NaturalGiftType.Name, token));
            descriptor.Field(x => x.GrowthTime)
                .Type<IntType>();
            descriptor.Field(x => x.Firmness)
                .Type<BerryFirmnessType>()
                .Resolver((ctx, token) => ctx.Service<BerryResolver>().GetBerryFirmnessAsync(ctx.Parent<Berry>().Firmness.Name, token));
            descriptor.Field(x => x.Item)
                .Description("Berries are actually items. This is a reference to the item specific data for this berry.")
                .Type<ItemType>()
                .Resolver((ctx, token) => ctx.Service<ItemResolver>().GetItemAsync(ctx.Parent<Berry>().Item.Name, token));
        }

        private sealed class BerryFlavorMapType : ObjectType<BerryFlavorMap>
        {
            /// <inheritdoc/>
            protected override void Configure(IObjectTypeDescriptor<BerryFlavorMap> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.Potency)
                    .Description("How powerful the referenced flavor is for this berry.");
                descriptor.Field(x => x.Flavor)
                    .Description("The referenced berry flavor.")
                    .Type<BerryFlavorType>()
                    .Resolver((ctx, token) => ctx.Service<BerryResolver>().GetBerryFlavorAsync(ctx.Parent<BerryFlavorMap>().Flavor.Name, token));
            }
        }
    }
}
