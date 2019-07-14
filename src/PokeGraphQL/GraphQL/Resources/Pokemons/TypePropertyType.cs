// <copyright file="TypePropertyType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using HotChocolate.Resolvers;
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Games;
    using PokeGraphQL.GraphQL.Resources.Moves;
    using TypeProperty = PokeApiNet.Models.Type;

    internal sealed class TypePropertyType : BaseNamedApiObjectType<TypeProperty>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<TypeProperty> descriptor)
        {
            descriptor.Description(@"Types are properties for Pokémon and their moves. 
                Each type has three properties: which types of Pokémon it is super effective against, which types of Pokémon it is not very effective against
                and which types of Pokémon it is completely ineffective against.");
            descriptor.Field(x => x.DamageRelations)
                .Description("A detail of how effective this type is toward others and vice versa.")
                .Type<TypeRelationsType>();
            descriptor.Field(x => x.GameIndices)
                .Description("A list of game indices relevent to this item by generation.")
                .Type<ListType<GenerationGameIndexType>>();
            descriptor.Field(x => x.Generation)
                .Description("The generation this type was introduced in.")
                .Type<GenerationType>()
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetGenerationAsync(ctx.Parent<TypeProperty>().Generation.Name, token));
            descriptor.Field(x => x.MoveDamageClass)
                .Description("The class of damage inflicted by this type.")
                .Type<MoveDamageClassType>()
                .Resolver((ctx, token) => ctx.Service<MoveResolver>().GetMoveDamageClassAsync(ctx.Parent<TypeProperty>().MoveDamageClass.Name, token));
            descriptor.Field(x => x.Pokemon)
                .Description("A list of details of pokemon that have this type.")
                .Type<ListType<TypePokemonType>>();
            descriptor.Field(x => x.Moves)
                .Description("A list of moves that have this type.")
                .Type<ListType<MoveType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<MoveResolver>();
                    var resourceTasks = ctx.Parent<TypeProperty>()
                        .Moves
                        .Select(move => resolver.GetMoveAsync(move.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
        }

        private sealed class TypeRelationsType : ObjectType<TypeRelations>
        {
            protected override void Configure(IObjectTypeDescriptor<TypeRelations> descriptor)
            {
                descriptor.Field(x => x.NoDamageTo)
                    .Description("A list of types this type has no effect on.")
                    .Type<ListType<TypePropertyType>>()
                    .Resolver(CreateResolver(source => source.NoDamageTo));
                descriptor.Field(x => x.HalfDamageTo)
                    .Description("A list of types this type is not very effect against.")
                    .Type<ListType<TypePropertyType>>()
                    .Resolver(CreateResolver(source => source.HalfDamageTo));
                descriptor.Field(x => x.DoubleDamageTo)
                    .Description("A list of types this type is very effect against.")
                    .Type<ListType<TypePropertyType>>()
                    .Resolver(CreateResolver(source => source.DoubleDamageTo));
                descriptor.Field(x => x.NoDamageFrom)
                    .Description("A list of types that have no effect on this type.")
                    .Type<ListType<TypePropertyType>>()
                    .Resolver(CreateResolver(source => source.NoDamageFrom));
                descriptor.Field(x => x.HalfDamageFrom)
                    .Description("A list of types that are not very effective against this type.")
                    .Type<ListType<TypePropertyType>>()
                    .Resolver(CreateResolver(source => source.HalfDamageFrom));
                descriptor.Field(x => x.DoubleDamageFrom)
                    .Description("A list of types that are very effective against this type.")
                    .Type<ListType<TypePropertyType>>()
                    .Resolver(CreateResolver(source => source.DoubleDamageFrom));
            }

            private static Func<IResolverContext, CancellationToken, Task<TypeProperty[]>> CreateResolver(Func<TypeRelations, List<NamedApiResource<TypeProperty>>> selector)
            => (ctx, token) =>
            {
                var resolver = ctx.Service<PokemonResolver>();
                var resourceTasks = selector(ctx.Parent<TypeRelations>())
                    .Select(type => resolver.GetTypeAsync(type.Name, token));
                return Task.WhenAll(resourceTasks);
            };
        }

        private sealed class TypePokemonType : ObjectType<TypePokemon>
        {
            protected override void Configure(IObjectTypeDescriptor<TypePokemon> descriptor)
            {
                descriptor.Field(x => x.Slot)
                    .Description("The order the pokemons types are listed in.");
                descriptor.Field(x => x.Pokemon)
                    .Description("The pokemon that has the referenced type.")
                    .Type<PokemonType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetPokemonAsync(ctx.Parent<TypePokemon>().Pokemon.Name, token));
            }
        }
    }
}
