// <copyright file="BerryType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Berries
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
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
            descriptor.UseNamedApiResourceField<Berry, Type, TypePropertyType>(x => x.NaturalGiftType);
            descriptor.UseNamedApiResourceField<Berry, BerryFirmness, BerryFirmnessType>(x => x.Firmness);
            descriptor.UseNamedApiResourceField<Berry, Item, ItemType>(x => x.Item);
        }

        private sealed class BerryFlavorMapType : ObjectType<BerryFlavorMap>
        {
            /// <inheritdoc/>
            protected override void Configure(IObjectTypeDescriptor<BerryFlavorMap> descriptor)
            {
                descriptor.Field(x => x.Potency)
                    .Description("How powerful the referenced flavor is for this berry.");
                descriptor.UseNamedApiResourceField<BerryFlavorMap, BerryFlavor, BerryFlavorType>(x => x.Flavor);
            }
        }
    }
}
