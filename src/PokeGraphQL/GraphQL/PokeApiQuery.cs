// <copyright file="PokeApiQuery.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Berries;
    using PokeGraphQL.GraphQL.Resources.Contests;
    using PokeGraphQL.GraphQL.Resources.Encounters;
    using PokeGraphQL.GraphQL.Resources.Evolutions;
    using PokeGraphQL.GraphQL.Resources.Games;
    using PokeGraphQL.GraphQL.Resources.Items;
    using PokeGraphQL.GraphQL.Resources.Languages;
    using PokeGraphQL.GraphQL.Resources.Locations;
    using PokeGraphQL.GraphQL.Resources.Machines;
    using PokeGraphQL.GraphQL.Resources.Moves;
    using PokeGraphQL.GraphQL.Resources.Pokemons;

    public sealed class PokeApiQuery : ObjectType
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name("Query").Description(@"All the Pokémon data you'll ever need in one place, 
                easily accessible through a modern GraphQL API.");
            RegisterBerryResources(descriptor);
            RegisterItemResources(descriptor);
            RegisterContestResources(descriptor);
            RegisterEncounterResources(descriptor);
            RegisterGameResources(descriptor);
            RegisterLocationResources(descriptor);
            RegisterPokemonResources(descriptor);
            RegisterMoveResources(descriptor);
            RegisterEvolutionResources(descriptor);
            RegisterLanguageResources(descriptor);
            RegisterMachineResources(descriptor);
        }

        private static void RegisterMachineResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("machine")
                .UseApiResource<Machine, MachineType>();
        }

        private static void RegisterLanguageResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("language")
                .UseNamedApiResource<Language, LanguageType>();
            descriptor.Field("languages")
                .UseNamedResourcePaging<Language, LanguageType>();
        }

        private static void RegisterEvolutionResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("evolutionChain")
                .UseApiResource<EvolutionChain, EvolutionChainType>();
        }

        private static void RegisterBerryResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("berry")
                .UseNamedApiResource<Berry, BerryType>();
            descriptor.Field("berries")
                .UseNamedResourcePaging<Berry, BerryType>();

            descriptor.Field("berryFirmness")
                .UseNamedApiResource<BerryFirmness, BerryFirmnessType>();

            descriptor.Field("berryFlavor")
                .UseNamedApiResource<BerryFlavor, BerryFlavorType>();
            descriptor.Field("berryFlavors")
                .UseNamedResourcePaging<BerryFlavor, BerryFlavorType>();
        }

        private static void RegisterItemResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("item")
                .UseNamedApiResource<Item, ItemType>();
            descriptor.Field("items")
                .UseNamedResourcePaging<Item, ItemType>();

            descriptor.Field("itemAttribute")
                .UseNamedApiResource<ItemAttribute, ItemAttributeType>();

            descriptor.Field("itemCategory")
                .UseNamedApiResource<ItemCategory, ItemCategoryType>();
            descriptor.Field("itemCategories")
                .UseNamedResourcePaging<ItemCategory, ItemCategoryType>();

            descriptor.Field("itemPocket")
                .UseNamedApiResource<ItemPocket, ItemPocketType>();

            descriptor.Field("itemFlingEffect")
                .UseNamedResourcePaging<ItemFlingEffect, ItemFlingEffectType>();
        }

        private static void RegisterContestResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("contestType")
                .UseNamedApiResource<ContestType, ContestTypeType>();
            descriptor.Field("contestTypes")
                .UseNamedResourcePaging<ContestType, ContestTypeType>();

            descriptor.Field("contestEffect")
                .UseApiResource<ContestEffect, ContestEffectType>();

            descriptor.Field("superContestEffect")
                .UseApiResource<SuperContestEffect, SuperContestEffectType>();
        }

        private static void RegisterEncounterResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("encounterMethod")
                .UseNamedApiResource<EncounterMethod, EncounterMethodType>();

            descriptor.Field("encounterCondition")
                .UseNamedApiResource<EncounterCondition, EncounterConditionType>();

            descriptor.Field("encounterConditionValue")
                .UseNamedApiResource<EncounterConditionValue, EncounterConditionValueType>();
        }

        private static void RegisterGameResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("generation")
                .UseNamedApiResource<Generation, GenerationType>();
            descriptor.Field("generations")
                .UseNamedResourcePaging<Generation, GenerationType>();

            descriptor.Field("pokedex")
                .UseNamedApiResource<Pokedex, PokedexType>();
            descriptor.Field("pokedexes")
                .UseNamedResourcePaging<Pokedex, PokedexType>();

            descriptor.Field("version")
                .UseNamedApiResource<Version, VersionType>();
            descriptor.Field("versions")
                .UseNamedResourcePaging<Version, VersionType>();

            descriptor.Field("versionGroup")
                .UseNamedApiResource<VersionGroup, VersionGroupType>();
            descriptor.Field("versionGroups")
                .UseNamedResourcePaging<Version, VersionType>();
        }

        private static void RegisterLocationResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("location")
                .UseNamedApiResource<Location, LocationType>();
            descriptor.Field("locations")
                .UseNamedResourcePaging<Location, LocationType>();

            descriptor.Field("locationArea")
                .UseNamedApiResource<LocationArea, LocationAreaType>();
            descriptor.Field("locationAreas")
                .UseNamedResourcePaging<LocationArea, LocationAreaType>();

            descriptor.Field("palParkArea")
                .UseNamedApiResource<PalParkArea, PalParkAreaType>();

            descriptor.Field("region")
                .UseNamedApiResource<Region, RegionType>();
            descriptor.Field("regions")
                .UseNamedResourcePaging<Region, RegionType>();
        }

        private static void RegisterPokemonResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("ability")
                .UseNamedApiResource<Ability, AbilityType>();
            descriptor.Field("abilities")
                .UseNamedResourcePaging<Ability, AbilityType>();

            descriptor.Field("characteristic")
                .UseApiResource<Characteristic, CharacteristicType>();

            descriptor.Field("eggGroup")
                .UseNamedApiResource<EggGroup, EggGroupType>();

            descriptor.Field("gender")
                .UseNamedApiResource<Gender, GenderType>();

            descriptor.Field("type")
                .UseNamedApiResource<Type, TypePropertyType>();
            descriptor.Field("types")
                .UseNamedResourcePaging<Type, TypePropertyType>();

            descriptor.Field("pokemonSpecies")
                .UseNamedApiResource<PokemonSpecies, PokemonSpeciesType>();
            descriptor.Field("pokemonSpeciesPage")
                .UseNamedResourcePaging<PokemonSpecies, PokemonSpeciesType>();

            descriptor.Field("pokemon")
                .UseNamedApiResource<Pokemon, Resources.Pokemons.PokemonType>();
            descriptor.Field("pokemons")
                .UseNamedResourcePaging<Pokemon, Resources.Pokemons.PokemonType>();
        }

        private static void RegisterMoveResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("move")
                 .UseNamedApiResource<Move, MoveType>();
            descriptor.Field("moves")
                .UseNamedResourcePaging<Move, MoveType>();

            descriptor.Field("moveAilment")
                 .UseNamedApiResource<MoveAilment, MoveAilmentType>();

            descriptor.Field("moveDamageClass")
                 .UseNamedApiResource<MoveDamageClass, MoveDamageClassType>();
        }
    }
}
