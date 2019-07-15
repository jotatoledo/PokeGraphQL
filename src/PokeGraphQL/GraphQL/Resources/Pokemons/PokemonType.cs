// <copyright file="PokemonType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
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
            descriptor.Field(x => x.Forms)
                .Description("A list of forms this pokémon can take on.")
                .Type<ListType<PokemonFormType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<PokemonResolver>();
                    var resourceTasks = ctx.Parent<Pokemon>()
                        .Forms
                        .Select(form => resolver.GetPokemonFormAsync(form.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
            descriptor.Field(x => x.Species)
                .Description("The species this pokémon belongs to.")
                .Type<PokemonSpeciesType>()
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetPokemonSpeciesAsync(ctx.Parent<Pokemon>().Species.Name, token));
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
                descriptor.Field(x => x.LocationArea)
                    .Type<LocationAreaType>()
                    .Resolver((ctx, token) => ctx.Service<LocationResolver>().GetLocationAreaAsync(ctx.Parent<LocationAreaEncounter>().LocationArea.Name, token));
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
                descriptor.Field(x => x.Stat)
                    .Type<StatType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetStatAsync(ctx.Parent<PokemonStat>().Stat.Name, token));
            }
        }

        private sealed class PokemonMoveVersionType : ObjectType<PokemonMoveVersion>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonMoveVersion> descriptor)
            {
                descriptor.Field(x => x.LevelLearnedAt);
                descriptor.Field(x => x.VersionGroup)
                    .Type<VersionGroupType>()
                    .Resolver((ctx, token) => ctx.Service<GameResolver>().GetVersionGroupAsync(ctx.Parent<PokemonMoveVersion>().VersionGroup.Name, token));
                descriptor.Field(x => x.MoveLearnMethod)
                    .Type<MoveLearnMethodType>()
                    .Resolver((ctx, token) => ctx.Service<MoveResolver>().GetMoveLearnMethodAsync(ctx.Parent<PokemonMoveVersion>().MoveLearnMethod.Name, token));
            }
        }

        private sealed class PokemonMoveType : ObjectType<PokemonMove>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonMove> descriptor)
            {
                descriptor.Field(x => x.Move)
                    .Type<MoveType>()
                    .Resolver((ctx, token) => ctx.Service<MoveResolver>().GetMoveAsync(ctx.Parent<PokemonMove>().Move.Name, token));
                descriptor.Field(x => x.VersionGroupDetails)
                    .Type<ListType<PokemonMoveVersionType>>();
            }
        }

        private sealed class PokemonHeldItemType : ObjectType<PokemonHeldItem>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonHeldItem> descriptor)
            {
                descriptor.Field(x => x.Item)
                    .Description("The item that may be holded.")
                    .Type<ItemType>()
                    .Resolver((ctx, token) => ctx.Service<ItemResolver>().GetItemAsync(ctx.Parent<PokemonHeldItem>().Item.Name, token));
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
                descriptor.Field(x => x.Ability)
                    .Description("The ability the pokémon may have.")
                    .Type<AbilityType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetAbilityAsync(ctx.Parent<PokemonAbility>().Ability.Name, token));
            }
        }

        private sealed class PokemonTypeMapType : ObjectType<PokeApiNet.Models.PokemonType>
        {
            protected override void Configure(IObjectTypeDescriptor<PokeApiNet.Models.PokemonType> descriptor)
            {
                descriptor.Field(x => x.Slot)
                    .Description("The order the pokémon types are listed in.");
                descriptor.Field(x => x.Type)
                    .Description("The type the referenced pokémon has.")
                    .Type<TypePropertyType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetTypeAsync(ctx.Parent<PokeApiNet.Models.PokemonType>().Type.Name, token));
            }
        }
    }
}
