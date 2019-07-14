// <copyright file="PokedexType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Games
{
    using System.Linq;
    using System.Threading.Tasks;
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
            descriptor.Field(x => x.Region)
                .Description("The region this pokédex catalogues pokémon for.")
                .Type<RegionType>()
                .Resolver((ctx, token) => ctx.Service<LocationResolver>().GetRegionAsync(ctx.Parent<Pokedex>().Region.Name, token));
            descriptor.Field(x => x.VersionGroups)
                .Description("A list of version groups this pokédex is relevent to.")
                .Type<ListType<VersionGroupType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<GameResolver>();
                    var resourceTasks = ctx.Parent<Pokedex>()
                        .VersionGroups
                        .Select(versionGroup => resolver.GetVersionGroupAsync(versionGroup.Name, token));
                    return Task.WhenAll(resourceTasks);
                });

            // TODO: implement ignored field
            descriptor.Field(x => x.Descriptions)
                .Description("The description of this pokédex listed in different languages.")
                .Ignore();
        }

        private sealed class PokemonEntryType : ObjectType<PokemonEntry>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonEntry> descriptor)
            {
                descriptor.Field(x => x.EntryNumber)
                    .Description("The index of this pokémon species entry within the pokédex.");
                descriptor.Field(x => x.PokemonSpecies)
                    .Description("The pokémon species being encountered.")
                    .Type<PokemonSpeciesType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetPokemonSpeciesAsync(ctx.Parent<PokemonEntry>().PokemonSpecies.Name, token));
            }
        }
    }
}
