﻿namespace PokeGraphQL.GraphQL.Resources.Locations
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;

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
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<LocationResolver>();
                    var resourceTasks = ctx.Parent<Location>()
                        .Areas
                        .Select(area => resolver.GetLocationAreaAsync(Convert.ToInt32(area.Url.LastSegment()), token));
                    return await Task.WhenAll(resourceTasks);
                });
        }
    }
}