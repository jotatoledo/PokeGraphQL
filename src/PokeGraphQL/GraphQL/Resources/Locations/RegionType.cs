// <copyright file="RegionType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Locations
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Games;

    internal sealed class RegionType : BaseNamedApiObjectType<Region>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Region> descriptor)
        {
            descriptor.Description(@"A region is an organized area of the pokémon world. 
            Most often, the main difference between regions is the species of pokémon that can be encountered within them.");
            descriptor.Field(x => x.Locations)
                .Description("A list of locations that can be found in this region.")
                .Type<ListType<LocationType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<LocationResolver>();
                    var resourceTasks = ctx.Parent<Region>()
                        .Locations
                        .Select(location => resolver.GetLocationAsync(location.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
            descriptor.Field(x => x.MainGeneration)
                .Description("The generation this region was introduced in.")
                .Type<GenerationType>()
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetGenerationAsync(ctx.Parent<Region>().MainGeneration.Name, token));
            descriptor.Field(x => x.Pokedexes)
                .Name("pokedexes")
                .Description("A list of pokédexes that catalogue pokemon in this region.")
                .Type<ListType<PokedexType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<GameResolver>();
                    var resourceTasks = ctx.Parent<Region>()
                        .Pokedexes
                        .Select(pokedex => resolver.GetPokedexAsync(pokedex.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
            descriptor.Field(x => x.VersionGroups)
                .Description("A list of version groups where this region can be visited.")
                .Type<ListType<VersionGroupType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<GameResolver>();
                    var resourceTasks = ctx.Parent<Region>()
                        .VersionGroups
                        .Select(versionGroup => resolver.GetVersionGroupAsync(versionGroup.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
        }
    }
}
