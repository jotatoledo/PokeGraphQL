// <copyright file="LocationAreaType.cs" company="PokeGraphQL.Net">
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
    using PokeGraphQL.GraphQL.Resources.Encounters;
    using PokeGraphQL.GraphQL.Resources.Games;
    using PokeGraphQL.GraphQL.Resources.Pokemons;
    using PokemonType = PokeGraphQL.GraphQL.Resources.Pokemons.PokemonType;

    internal sealed class LocationAreaType : BaseNamedApiObjectType<LocationArea>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<LocationArea> descriptor)
        {
            descriptor.Description(@"Location areas are sections of areas, such as floors in a building or cave. 
                Each area has its own set of possible pokemon encounters.");
            descriptor.Field(x => x.GameIndex)
                .Description("The internal id of an api resource within game data.");
            descriptor.Field(x => x.EncounterMethodRates)
                .Description(@"A list of methods in which pokémon may be encountered in this area 
                    and how likely the method will occur depending on the version of the game.")
                .Type<ListType<EncounterMethodRateType>>();
            descriptor.Field(x => x.Location)
                .Description("The region this location can be found in")
                .Type<LocationType>()
                .Resolver((ctx, token) => ctx.Service<LocationResolver>().GetLocationAsync(ctx.Parent<LocationArea>().Location.Name, token));
            descriptor.Field(x => x.PokemonEncounters)
                .Description("A list of pokémon that can be encountered in this area along with version specific details about the encounter.")
                .Type<ListType<PokemonEncounterType>>();
        }

        private sealed class PokemonEncounterType : ObjectType<PokemonEncounter>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonEncounter> descriptor)
            {
                descriptor.Field(x => x.Pokemon)
                    .Description("The pokémon being encountered.")
                    .Type<PokemonType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetPokemonAsync(ctx.Parent<PokemonEncounter>().Pokemon.Name, token));
                descriptor.Field(x => x.VersionDetails)
                    .Description("A list of versions and encounters with pokémon that might happen in the referenced location area.")
                    .Type<ListType<VersionEncounterDetailType>>();
            }
        }

        private sealed class EncounterVersionDetailsType : ObjectType<EncounterVersionDetails>
        {
            protected override void Configure(IObjectTypeDescriptor<EncounterVersionDetails> descriptor)
            {
                descriptor.Field(x => x.Rate)
                    .Description("The chance of an encounter to occur.");
                descriptor.Field(x => x.Version)
                    .Description("The version of the game in which the encounter can occur with the given chance.")
                    .Type<VersionType>();
            }
        }

        private sealed class EncounterMethodRateType : ObjectType<EncounterMethodRate>
        {
            protected override void Configure(IObjectTypeDescriptor<EncounterMethodRate> descriptor)
            {
                descriptor.Field(x => x.EncounterMethod)
                    .Description("The method in which pokémon may be encountered in an area.")
                    .Type<EncounterMethodType>()
                    .Resolver((ctx, token) => ctx.Service<EncounterResolver>()
                        .GetEncounterMethodAsync(ctx.Parent<EncounterMethodRate>().EncounterMethod.Name, token));
                descriptor.Field(x => x.VersionDetails)
                    .Description("The chance of the encounter to occur on a version of the game.")
                    .Type<ListType<EncounterVersionDetailsType>>();
            }
        }
    }
}
