﻿// <copyright file="BerryFlavorType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

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
                descriptor.FixStructType();
                descriptor.Field(x => x.Potency);
                descriptor.Field(x => x.Berry)
                    .Description("The berry with the referenced flavor.")
                    .Type<BerryType>()
                    .Resolver((ctx, token) => ctx.Service<BerryResolver>().GetBerryAsync(ctx.Parent<FlavorBerryMap>().Berry.Name, token));
            }
        }
    }
}
