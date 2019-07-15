// <copyright file="LocationType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Locations
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Common;

    internal sealed class LocationType : BaseNamedApiObjectType<Location>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Location> descriptor)
        {
            descriptor.Description("Locations that can be visited within the games. Locations make up sizable portions of regions, like cities or routes.");
            descriptor.Field(x => x.Region)
                .Description("The region this location can be found in.")
                .Type<RegionType>()
                .Resolver((ctx, token) => ctx.Service<LocationResolver>().GetRegionAsync(ctx.Parent<Location>().Region.Name, token));
            descriptor.Field(x => x.GameIndices)
                .Description("A list of game indices relevent to this location by generation.")
                .Type<ListType<GenerationGameIndexType>>();
            descriptor.Field(x => x.Areas)
                .Description("Areas that can be found within this location.")
                .Type<ListType<LocationAreaType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<LocationResolver>();
                    var resourceTasks = ctx.Parent<Location>()
                        .Areas
                        .Select(area => resolver.GetLocationAreaAsync(area.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
        }
    }
}
