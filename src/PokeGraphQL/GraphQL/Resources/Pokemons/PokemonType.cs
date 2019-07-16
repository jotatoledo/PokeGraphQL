// <copyright file="PokemonType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Common;
    using PokeGraphQL.GraphQL.Resources.Games;
    using PokeGraphQL.GraphQL.Resources.Items;
    using PokeGraphQL.GraphQL.Resources.Locations;
    using PokeGraphQL.GraphQL.Resources.Moves;

    internal sealed class PokemonType : BaseNamedApiObjectType<Pokemon>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Pokemon> descriptor)
        {
            descriptor.Description(@"Pokémon are the creatures that inhabit the world of the pokemon games. 
                They can be caught using pokéballs and trained by battling with other pokémon.");
            descriptor.Ignore(x => x.Sprites);
            descriptor.Field(x => x.BaseExperience)
                .Description("The base experience gained for defeating this pokémon.");
            descriptor.Field(x => x.Height)
                .Description("The height of this pokémon.");
            descriptor.Field(x => x.IsDefault)
                .Description("Set for exactly one pokémon used as the default for each species.");
            descriptor.Field(x => x.Order)
                .Description("Order for sorting. Almost national order, except families are grouped together.");
            descriptor.Field(x => x.Weight)
                .Description("The mass of this pokémon.");
            descriptor.Field(x => x.Abilities)
                .Description("A list of abilities this pokémon could potentially have.")
                .Type<ListType<PokemonAbilityType>>();
            descriptor.UseNamedApiResourceCollectionField<Pokemon, PokemonForm, PokemonFormType>(x => x.Forms);
            descriptor.UseNamedApiResourceField<Pokemon, PokemonSpecies, PokemonSpeciesType>(x => x.Species);
            descriptor.Field(x => x.Types)
                .Description("A list of details showing types this pokémon has.")
                .Type<ListType<PokemonTypeMapType>>();
            descriptor.Field(x => x.HeldItems)
                .Description("A list of items this pokémon may be holding when encountered.")
                .Type<ListType<PokemonHeldItemType>>();
            descriptor.Field(x => x.Moves)
                .Description("A list of moves along with learn methods and level details pertaining to specific version groups.")
                .Type<ListType<PokemonMoveType>>();
            descriptor.Field(x => x.Stats)
                .Description("A list of base stat values for this pokémon.")
                .Type<ListType<PokemonStatType>>();
            descriptor.Field(x => x.GameIndicies)
                .Description("A list of game indices relevent to pokémon item by generation.")
                .Type<ListType<VersionGameIndexType>>();
            descriptor.Field(x => x.LocationAreaEncounters)
                .Description("A list of location areas as well as encounter details pertaining to specific versions.")
                .Type<ListType<LocationAreaEncounterType>>()
                .Resolver((ctx, token) => ctx.Service<UrlResolver>().GetAsync<LocationAreaEncounter[]>(ctx.Parent<Pokemon>().LocationAreaEncounters));
        }

        private sealed class LocationAreaEncounterType : ObjectType<LocationAreaEncounter>
        {
            protected override void Configure(IObjectTypeDescriptor<LocationAreaEncounter> descriptor)
            {
                descriptor.UseNamedApiResourceField<LocationAreaEncounter, LocationArea, LocationAreaType>(x => x.LocationArea);
                descriptor.Field(x => x.VersionDetails)
                    .Type<ListType<VersionEncounterDetailType>>();
            }
        }

        private sealed class PokemonStatType : ObjectType<PokemonStat>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonStat> descriptor)
            {
                descriptor.Field(x => x.BaseStat);
                descriptor.Field(x => x.Effort);
                descriptor.UseNamedApiResourceField<PokemonStat, Stat, StatType>(x => x.Stat);
            }
        }

        private sealed class PokemonMoveVersionType : ObjectType<PokemonMoveVersion>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonMoveVersion> descriptor)
            {
                descriptor.Field(x => x.LevelLearnedAt);
                descriptor.UseNamedApiResourceField<PokemonMoveVersion, VersionGroup, VersionGroupType>(x => x.VersionGroup);
                descriptor.UseNamedApiResourceField<PokemonMoveVersion, MoveLearnMethod, MoveLearnMethodType>(x => x.MoveLearnMethod);
            }
        }

        private sealed class PokemonMoveType : ObjectType<PokemonMove>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonMove> descriptor)
            {
                descriptor.UseNamedApiResourceField<PokemonMove, Move, MoveType>(x => x.Move);
                descriptor.Field(x => x.VersionGroupDetails)
                    .Type<ListType<PokemonMoveVersionType>>();
            }
        }

        private sealed class PokemonHeldItemType : ObjectType<PokemonHeldItem>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonHeldItem> descriptor)
            {
                descriptor.UseNamedApiResourceField<PokemonHeldItem, Item, ItemType>(x => x.Item);
                descriptor.Field(x => x.VersionDetails)
                    .Description("Details on chance of the pokemon having the item based on version.")
                    .Type<ListType<HeldItemVersionDetailsType>>();
            }
        }

        private sealed class PokemonAbilityType : ObjectType<PokemonAbility>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonAbility> descriptor)
            {
                descriptor.Field(x => x.IsHidden)
                    .Description("Whether or not this is a hidden ability.");
                descriptor.Field(x => x.Slot)
                    .Description("The slot this ability occupies in this pokémon species.");
                descriptor.UseNamedApiResourceField<PokemonAbility, Ability, AbilityType>(x => x.Ability);
            }
        }

        private sealed class PokemonTypeMapType : ObjectType<PokeApiNet.Models.PokemonType>
        {
            protected override void Configure(IObjectTypeDescriptor<PokeApiNet.Models.PokemonType> descriptor)
            {
                descriptor.Field(x => x.Slot)
                    .Description("The order the pokémon types are listed in.");
                descriptor.UseNamedApiResourceField<PokeApiNet.Models.PokemonType, Type, TypePropertyType>(x => x.Type);
            }
        }
    }
}
