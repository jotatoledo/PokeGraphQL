// <copyright file="PokedexType.cs" company="PokeGraphQL.Net">
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
    using PokeGraphQL.GraphQL.Resources.Pokemons;

    internal sealed class PokedexType : BaseNamedApiObjectType<Pokedex>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Pokedex> descriptor)
        {
            descriptor.Description(@"A Pokédex is a handheld electronic encyclopedia device; one which is capable of recording 
            and retaining information of the various Pokémon in a given region with the exception of the national dex
            and some smaller dexes related to portions of a region.");
            descriptor.Field(x => x.IsMainSeries)
                .Description("Whether or not this pokédex originated in the main series of the video games.");
            descriptor.Field(x => x.PokemonEntries)
                .Description("A list of pokémon catalogued in this pokédex  and their indexes.")
                .Type<ListType<PokemonEntryType>>();
            descriptor.UseNamedApiResourceField<Pokedex, Region, RegionType>(x => x.Region);
            descriptor.UseNamedApiResourceCollectionField<Pokedex, VersionGroup, VersionGroupType>(x => x.VersionGroups);

            // TODO: implement ignored field
            descriptor.Ignore(x => x.Descriptions);
        }

        private sealed class PokemonEntryType : ObjectType<PokemonEntry>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonEntry> descriptor)
            {
                descriptor.Field(x => x.EntryNumber)
                    .Description("The index of this pokémon species entry within the pokédex.");
                descriptor.UseNamedApiResourceField<PokemonEntry, PokemonSpecies, PokemonSpeciesType>(x => x.PokemonSpecies);
            }
        }
    }
}
