// <copyright file="StatType.cs" company="PokeGraphQL.Net">
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

    internal sealed class StatType : BaseNamedApiObjectType<Stat>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Stat> descriptor)
        {
            // TODO implement ignored fields
            descriptor.Description(@"Stats determine certain aspects of battles. 
                Each pokémon has a value for each stat which grows as they gain levels and can be altered momenarily by effects in battles.");
            descriptor.Field(x => x.GameIndex)
                .Description("ID the games use for this stat.");
            descriptor.Field(x => x.IsBattleOnly)
                .Description("Whether this stat only exists within a battle.");
            descriptor.Field(x => x.AffectingMoves)
                .Description("A detail of moves which affect this stat positively or negatively.")
                .Type<MoveStatAffectSetType>();
            descriptor.Field(x => x.AffectingNatures)
                .Description("A detail of natures which affect this stat positively or negatively.")
                .Type<NatureStatAffectType>();
            descriptor.Field(x => x.Characteristics)
                .Description("A list of characteristics that are set on a pokemon when its highest base stat is this stat.")
                .Type<ListType<CharacteristicType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<PokemonResolver>();
                    var resourceTasks = ctx.Parent<Stat>()
                        .Characteristics
                        .Select(characteristic => resolver.GetCharacteristicAsync(Convert.ToInt32(characteristic.Url.LastSegment()), token));
                    return Task.WhenAll(resourceTasks);
                });
            descriptor.Field(x => x.MoveDamageClass)
                .Description("The class of damage this stat is directly related to.")
                .Ignore();
        }

        private sealed class NatureStatAffectType : ObjectType<StatAffectNature>
        {
            protected override void Configure(IObjectTypeDescriptor<StatAffectNature> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.Increase)
                    .Description("A list of natures and how they change the referenced stat.")
                    .Type<ListType<NatureType>>()
                    .Resolver((ctx, token) =>
                    {
                        var resolver = ctx.Service<PokemonResolver>();
                        var resourceTasks = ctx.Parent<StatAffectNature>()
                            .Increase
                            .Select(nature => resolver.GetNatureAsync(nature.Name, token));
                        return Task.WhenAll(resourceTasks);
                    });
                descriptor.Field(x => x.Decrease)
                    .Description("A list of natures and how they change the referenced stat.")
                    .Type<ListType<NatureType>>()
                    .Resolver((ctx, token) =>
                    {
                        var resolver = ctx.Service<PokemonResolver>();
                        var resourceTasks = ctx.Parent<StatAffectNature>()
                            .Decrease
                            .Select(nature => resolver.GetNatureAsync(nature.Name, token));
                        return Task.WhenAll(resourceTasks);
                    });
            }
        }

        private sealed class MoveStatAffectType : ObjectType<StatAffect<Move>>
        {
            protected override void Configure(IObjectTypeDescriptor<StatAffect<Move>> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.Change)
                    .Description("The maximum amount of change to the referenced stat.");

                // TODO implement ignored field
                descriptor.Field(x => x.Resource)
                    .Name("move")
                    .Description("The move causing the change.")
                    .Ignore();
            }
        }

        private sealed class MoveStatAffectSetType : ObjectType<StatAffectSets<Move>>
        {
            protected override void Configure(IObjectTypeDescriptor<StatAffectSets<Move>> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.Increase)
                    .Description("A list of moves and how they change the referenced stat.")
                    .Type<ListType<MoveStatAffectType>>();
                descriptor.Field(x => x.Decrease)
                    .Description("A list of moves and how they change the referenced stat.")
                    .Type<ListType<MoveStatAffectType>>();
            }
        }
    }
}
