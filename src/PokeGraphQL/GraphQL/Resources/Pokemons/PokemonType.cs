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
    using PokeAPI;
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
            descriptor.Field(x => x.Mass)
                .Name("weight")
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
            descriptor.Field(x => x.GameIndices)
                .Description("A list of game indices relevent to pokémon item by generation.")
                .Type<ListType<VersionGameIndexType>>();

            // TODO unignore once a new version of PokeAPI is released
            descriptor.Field(x => x.LocationAreaEncounters)
                .Description("A list of location areas as well as encounter details pertaining to specific versions.")
                .Type<ListType<LocationAreaEncounterType>>()
                .Resolver((ctx, token) => ctx.Service<UrlResolver>().GetAsync<LocationAreaEncounter[]>(ctx.Parent<Pokemon>().LocationAreaEncounters.Path))
                .Ignore();
        }

        private sealed class LocationAreaEncounterType : ObjectType<LocationAreaEncounter>
        {
            protected override void Configure(IObjectTypeDescriptor<LocationAreaEncounter> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.LocationArea)
                    .Type<LocationAreaType>()
                    .Resolver((ctx, token) => ctx.Service<LocationResolver>().GetLocationAreaAsync(Convert.ToInt32(ctx.Parent<LocationAreaEncounter>().LocationArea.Url.LastSegment()), token));
                descriptor.Field(x => x.VersionDetails)
                    .Type<ListType<VersionEncounterDetailType>>();
            }
        }

        private sealed class PokemonStatType : ObjectType<PokemonStats>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonStats> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.BaseValue);
                descriptor.Field(x => x.Effort);
                descriptor.Field(x => x.Stat)
                    .Type<StatType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetStatAsync(ctx.Parent<PokemonStats>().Stat.Name, token));
            }
        }

        private sealed class MoveVersionGroupDetailType : ObjectType<MoveVersionGroupDetails>
        {
            protected override void Configure(IObjectTypeDescriptor<MoveVersionGroupDetails> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.LearnedAt);
                descriptor.Field(x => x.VersionGroup)
                    .Type<VersionGroupType>()
                    .Resolver((ctx, token) => ctx.Service<GameResolver>().GetVersionGroupAsync(ctx.Parent<MoveVersionGroupDetails>().VersionGroup.Name, token));
                descriptor.Field(x => x.LearnMethod)
                    .Type<MoveLearnMethodType>()
                    .Resolver((ctx, token) => ctx.Service<MoveResolver>().GetMoveLearnMethodAsync(ctx.Parent<MoveVersionGroupDetails>().LearnMethod.Name, token));
            }
        }

        private sealed class PokemonMoveType : ObjectType<PokemonMove>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonMove> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.Move)
                    .Type<MoveType>()
                    .Resolver((ctx, token) => ctx.Service<MoveResolver>().GetMoveAsync(ctx.Parent<PokemonMove>().Move.Name, token));
                descriptor.Field(x => x.VersionGroupDetails)
                    .Type<ListType<MoveVersionGroupDetailType>>();
            }
        }

        private sealed class PokemonHeldItemType : ObjectType<PokemonHeldItem>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonHeldItem> descriptor)
            {
                descriptor.FixStructType();
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
                descriptor.FixStructType();
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

        private sealed class PokemonTypeMapType : ObjectType<PokemonTypeMap>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonTypeMap> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.Slot)
                    .Description("The order the pokémon types are listed in.");
                descriptor.Field(x => x.Type)
                    .Description("The type the referenced pokémon has.")
                    .Type<TypePropertyType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetTypeAsync(ctx.Parent<PokemonTypeMap>().Type.Name, token));
            }
        }
    }
}
