// <copyright file="GenerationType.cs" company="PokeGraphQL.Net">
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
    using PokeAPI;
    using PokeGraphQL.GraphQL.Resources.Locations;
    using PokeGraphQL.GraphQL.Resources.Moves;
    using PokeGraphQL.GraphQL.Resources.Pokemons;

    internal sealed class GenerationType : BaseNamedApiObjectType<Generation>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Generation> descriptor)
        {
            // TODO implemet ignored fields
            descriptor.Description(@"A generation is a grouping of the Pokémon games that separates them based on the Pokémon they include.
            In each generation, a new set of Pokémon, Moves, Abilities and Types that did not exist in the previous generation are released.");
            descriptor.Field(x => x.Abilities)
                .Description("A list of abilities that were introduced in this generation.")
                .Type<ListType<AbilityType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<PokemonResolver>();
                    var resourceTasks = ctx.Parent<Generation>()
                        .Abilities
                        .Select(ability => resolver.GetAbilityAsync(ability.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
            descriptor.Field(x => x.MainRegion)
                .Description("The main region travelled in this generation.")
                .Type<RegionType>()
                .Resolver((ctx, token) => ctx.Service<LocationResolver>().GetRegionAsync(ctx.Parent<Generation>().MainRegion.Name, token));
            descriptor.Field(x => x.Moves)
                .Description("A list of moves that were introduced in this generation.")
                .Type<ListType<MoveType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<MoveResolver>();
                    var resourceTasks = ctx.Parent<Generation>()
                        .Moves
                        .Select(move => resolver.GetMoveAsync(move.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
            descriptor.Field(x => x.Species)
                .Name("pokemonSpecies")
                .Description("A list of pokémon species that were introduced in this generation.")
                .Type<ListType<PokemonSpeciesType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<PokemonResolver>();
                    var resourceTasks = ctx.Parent<Generation>()
                        .Species
                        .Select(species => resolver.GetPokemonSpeciesAsync(species.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
            descriptor.Field(x => x.Types)
                .Description("A list of types that were introduced in this generation.")
                .Type<ListType<TypePropertyType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<PokemonResolver>();
                    var resourceTasks = ctx.Parent<Generation>()
                        .Types
                        .Select(type => resolver.GetTypeAsync(type.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
            descriptor.Field(x => x.VersionGroups)
                .Description("A list of version groups that were introduced in this generation.")
                .Type<ListType<VersionGroupType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<GameResolver>();
                    var resourceTasks = ctx.Parent<Generation>()
                    .VersionGroups
                    .Select(versionGroup => resolver.GetVersionGroupAsync(versionGroup.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
        }
    }
}
