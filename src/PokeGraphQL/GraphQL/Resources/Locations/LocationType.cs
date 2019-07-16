// <copyright file="LocationType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Locations
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Common;

    internal sealed class LocationType : BaseNamedApiObjectType<Location>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Location> descriptor)
        {
            descriptor.Description("Locations that can be visited within the games. Locations make up sizable portions of regions, like cities or routes.");
            descriptor.UseNamedApiResourceField<Location, Region, RegionType>(x => x.Region);
            descriptor.Field(x => x.GameIndices)
                .Description("A list of game indices relevent to this location by generation.")
                .Type<ListType<GenerationGameIndexType>>();
            descriptor.UseNamedApiResourceCollectionField<Location, LocationArea, LocationAreaType>(x => x.Areas);
        }
    }
}
