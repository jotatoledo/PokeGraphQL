// <copyright file="RegionType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Locations
{
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
            descriptor.UseNamedApiResourceCollectionField<Region, Location, LocationType>(x => x.Locations);
            descriptor.UseNamedApiResourceField<Region, Generation, GenerationType>(x => x.MainGeneration);
            descriptor.UseNamedApiResourceCollectionField<Region, Pokedex, PokedexType>(x => x.Pokedexes);
            descriptor.UseNamedApiResourceCollectionField<Region, VersionGroup, VersionGroupType>(x => x.VersionGroups);
        }
    }
}
