// <copyright file="BerryFlavorType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Berries
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Contests;

    internal sealed class BerryFlavorType : BaseNamedApiObjectType<BerryFlavor>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<BerryFlavor> descriptor)
        {
            descriptor.Description("Flavors determine whether a Pokémon will benefit or suffer from eating a berry based on their nature.");
            descriptor.UseNamedApiResourceField<BerryFlavor, ContestType, ContestTypeType>(x => x.ContestType);
            descriptor.Field(x => x.Berries)
                .Type<ListType<FlavorBerryMapType>>();
        }

        private sealed class FlavorBerryMapType : ObjectType<FlavorBerryMap>
        {
            /// <inheritdoc/>
            protected override void Configure(IObjectTypeDescriptor<FlavorBerryMap> descriptor)
            {
                descriptor.Field(x => x.Potency);
                descriptor.UseNamedApiResourceField<FlavorBerryMap, Berry, BerryType>(x => x.Berry);
            }
        }
    }
}
