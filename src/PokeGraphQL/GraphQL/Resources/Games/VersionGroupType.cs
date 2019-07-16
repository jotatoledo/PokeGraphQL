// <copyright file="VersionGroupType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Games
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Locations;
    using PokeGraphQL.GraphQL.Resources.Moves;

    internal sealed class VersionGroupType : BaseNamedApiObjectType<VersionGroup>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<VersionGroup> descriptor)
        {
            descriptor.Description("Version groups categorize highly similar versions of the games.");
            descriptor.Field(x => x.Order)
                .Description("Order for sorting. Almost by date of release, except similar versions are grouped together.");
            descriptor.UseNamedApiResourceField<VersionGroup, Generation, GenerationType>(x => x.Generation);
            descriptor.UseNamedApiResourceCollectionField<VersionGroup, MoveLearnMethod, MoveLearnMethodType>(x => x.MoveLearnMethods);
            descriptor.UseNamedApiResourceCollectionField<VersionGroup, Pokedex, PokedexType>(x => x.Pokedexes);
            descriptor.UseNamedApiResourceCollectionField<VersionGroup, Region, RegionType>(x => x.Regions);
            descriptor.UseNamedApiResourceCollectionField<VersionGroup, Version, VersionType>(x => x.Versions);
        }
    }
}
