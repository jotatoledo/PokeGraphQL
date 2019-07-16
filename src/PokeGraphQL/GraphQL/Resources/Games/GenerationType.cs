// <copyright file="GenerationType.cs" company="PokeGraphQL.Net">
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
    using PokeGraphQL.GraphQL.Resources.Pokemons;

    internal sealed class GenerationType : BaseNamedApiObjectType<Generation>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Generation> descriptor)
        {
            descriptor.Description(@"A generation is a grouping of the Pokémon games that separates them based on the Pokémon they include.
                In each generation, a new set of Pokémon, Moves, Abilities and Types that did not exist in the previous generation are released.");
            descriptor.UseNamedApiResourceCollectionField<Generation, Ability, AbilityType>(x => x.Abilities);
            descriptor.UseNamedApiResourceField<Generation, Region, RegionType>(x => x.MainRegion);
            descriptor.UseNamedApiResourceCollectionField<Generation, Move, MoveType>(x => x.Moves);
            descriptor.UseNamedApiResourceCollectionField<Generation, PokemonSpecies, PokemonSpeciesType>(x => x.PokemonSpecies);
            descriptor.UseNamedApiResourceCollectionField<Generation, Type, TypePropertyType>(x => x.Types);
            descriptor.UseNamedApiResourceCollectionField<Generation, VersionGroup, VersionGroupType>(x => x.VersionGroups);
        }
    }
}
