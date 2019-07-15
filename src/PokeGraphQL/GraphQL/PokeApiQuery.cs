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
                .Type<MachineType>()
                .Argument("id", a => a.Type<IntType>().Description("The identifier for the resource."))
                .Resolver((ctx, token) => ctx.Service<MachineResolver>().GetMachineAsync(ctx.Argument<int>("id"), token));
        }

        private static void RegisterLanguageResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("language")
                .Type<LanguageType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<LanguageResolver>().GetLanguageAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("languages")
                .UseNamedResourcePaging<Language, LanguageType>();
        }

        private static void RegisterEvolutionResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("evolutionChain")
                .Type<SuperContestEffectType>()
                .Argument("id", a => a.Type<IntType>().Description("The identifier for the resource."))
                .Resolver((ctx, token) => ctx.Service<EvolutionResolver>().GetEvolutionChainAsync(ctx.Argument<int>("id"), token));
        }

        private static void RegisterBerryResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("berry")
                .Type<BerryType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<BerryResolver>().GetBerryAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("berries")
                .UseNamedResourcePaging<Berry, BerryType>();

            descriptor.Field("berryFirmness")
                .Type<BerryFirmnessType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<BerryResolver>().GetBerryFirmnessAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("berryFlavor")
                .Type<BerryFlavorType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<BerryResolver>().GetBerryFlavorAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("berryFlavors")
                .UseNamedResourcePaging<BerryFlavor, BerryFlavorType>();
        }

        private static void RegisterItemResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("item")
                .Type<ItemType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<ItemResolver>().GetItemAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("items")
                .UseNamedResourcePaging<Item, ItemType>();

            descriptor.Field("itemAttribute")
                .Type<ItemAttributeType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<ItemResolver>().GetAttributeAsync(ctx.Argument<string>("nameOrId"), token));

            descriptor.Field("itemCategory")
                .Type<ItemCategoryType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<ItemResolver>().GetCategoryAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("itemCategories")
                .UseNamedResourcePaging<ItemCategory, ItemCategoryType>();

            descriptor.Field("itemPocket")
                .Type<ItemPocketType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<ItemResolver>().GetPocketAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("itemFlingEffect")
                .Type<ItemFlingEffectType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<ItemResolver>().GetFlingEffectAsync(ctx.Argument<string>("nameOrId"), token));
        }

        private static void RegisterContestResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("contestType")
                .Type<ContestTypeType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<ContestResolver>().GetContestTypeAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("contestTypes")
                .UseNamedResourcePaging<ContestType, ContestTypeType>();

            descriptor.Field("contestEffect")
                .Type<ContestEffectType>()
                .Argument("id", a => a.Type<IntType>().Description("The identifier for the resource."))
                .Resolver((ctx, token) => ctx.Service<ContestResolver>().GetContestEffectAsync(ctx.Argument<int>("id"), token));
            descriptor.Field("superContestEffect")
                .Type<SuperContestEffectType>()
                .Argument("id", a => a.Type<IntType>().Description("The identifier for the resource."))
                .Resolver((ctx, token) => ctx.Service<ContestResolver>().GetSuperContestEffectAsync(ctx.Argument<int>("id"), token));
        }

        private static void RegisterEncounterResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("encounterMethod")
                .Type<EncounterMethodType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<EncounterResolver>().GetEncounterMethodAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("encounterCondition")
                .Type<EncounterConditionType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<EncounterResolver>().GetEncounterConditionAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("encounterConditionValue")
                .Type<EncounterConditionValueType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<EncounterResolver>().GetEncounterConditionValueAsync(ctx.Argument<string>("nameOrId"), token));
        }

        private static void RegisterGameResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("generation")
                .Type<GenerationType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetGenerationAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("generations")
                .UseNamedResourcePaging<Generation, GenerationType>();

            descriptor.Field("pokedex")
                .Type<PokedexType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetPokedexAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("pokedexes")
                .UseNamedResourcePaging<Pokedex, PokedexType>();

            descriptor.Field("version")
                .Type<VersionType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetVersionAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("versions")
                .UseNamedResourcePaging<Version, VersionType>();

            descriptor.Field("versionGroup")
                .Type<VersionGroupType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetVersionGroupAsync(ctx.Argument<string>("nameOrId"), token));
        }

        private static void RegisterLocationResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("location")
                .Type<LocationType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<LocationResolver>().GetLocationAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("locations")
                .UseNamedResourcePaging<Location, LocationType>();

            descriptor.Field("locationArea")
                .Type<LocationAreaType>()
                .Argument("nameOrId", a => a.Type<IntType>().Description("The identifier for the resource."))
                .Resolver((ctx, token) => ctx.Service<LocationResolver>().GetLocationAreaAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("locationAreas")
                .UseNamedResourcePaging<LocationArea, LocationAreaType>();

            descriptor.Field("palParkArea")
                .Type<PalParkAreaType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<LocationResolver>().GetPalParkAreaAsync(ctx.Argument<string>("nameOrId"), token));

            descriptor.Field("region")
                .Type<RegionType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<LocationResolver>().GetRegionAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("regions")
                .UseNamedResourcePaging<Region, RegionType>();
        }

        private static void RegisterPokemonResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("ability")
                .Type<AbilityType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetAbilityAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("abilities")
                .UseNamedResourcePaging<Ability, AbilityType>();

            descriptor.Field("characteristic")
                .Type<CharacteristicType>()
                .Argument("id", a => a.Type<StringType>().Description("The identifier for the resource."))
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetCharacteristicAsync(ctx.Argument<int>("id"), token));
            descriptor.Field("eggGroup")
                .Type<EggGroupType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetEggGroupAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("gender")
                .Type<GenderType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetGenderAsync(ctx.Argument<string>("nameOrId"), token));

            descriptor.Field("type")
                .Type<TypePropertyType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetTypeAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("types")
                .UseNamedResourcePaging<Type, TypePropertyType>();

            descriptor.Field("pokemonSpecies")
                .Type<PokemonSpeciesType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetPokemonSpeciesAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("pokemonSpeciesPage")
                .UseNamedResourcePaging<PokemonSpecies, PokemonSpeciesType>();

            descriptor.Field("pokemon")
                .Type<Resources.Pokemons.PokemonType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetPokemonAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("pokemons")
                .UseNamedResourcePaging<Pokemon, Resources.Pokemons.PokemonType>();
        }

        private static void RegisterMoveResources(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("move")
                .Type<MoveType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<MoveResolver>().GetMoveAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("moves")
                .UseNamedResourcePaging<Move, MoveType>();
        }
    }
}
