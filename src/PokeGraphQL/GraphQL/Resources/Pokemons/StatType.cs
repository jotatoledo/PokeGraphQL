// <copyright file="StatType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Moves;

    internal sealed class StatType : BaseNamedApiObjectType<Stat>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Stat> descriptor)
        {
            descriptor.Description(@"Stats determine certain aspects of battles. 
                Each pokémon has a value for each stat which grows as they gain levels and can be altered momenarily by effects in battles.");
            descriptor.Field(x => x.GameIndex)
                .Description("ID the games use for this stat.");
            descriptor.Field(x => x.IsBattleOnly)
                .Description("Whether this stat only exists within a battle.");
            descriptor.Field(x => x.AffectingMoves)
                .Description("A detail of moves which affect this stat positively or negatively.")
                .Type<MoveStatAffectSetsType>();
            descriptor.Field(x => x.AffectingNatures)
                .Description("A detail of natures which affect this stat positively or negatively.")
                .Type<NatureStatAffectSetsType>();
            descriptor.UseApiResourceCollectionField<Stat, Characteristic, CharacteristicType>(x => x.Characteristics);
            descriptor.UseNamedApiResourceField<Stat, MoveDamageClass, MoveDamageClassType>(x => x.MoveDamageClass);
        }

        private sealed class NatureStatAffectSetsType : ObjectType<NatureStatAffectSets>
        {
            protected override void Configure(IObjectTypeDescriptor<NatureStatAffectSets> descriptor)
            {
                descriptor.UseNamedApiResourceCollectionField<NatureStatAffectSets, Nature, NatureType>(x => x.Increase);
                descriptor.UseNamedApiResourceCollectionField<NatureStatAffectSets, Nature, NatureType>(x => x.Decrease);
            }
        }

        private sealed class MoveStatAffectType : ObjectType<MoveStatAffect>
        {
            protected override void Configure(IObjectTypeDescriptor<MoveStatAffect> descriptor)
            {
                descriptor.Field(x => x.Change)
                    .Description("The maximum amount of change to the referenced stat.");
                descriptor.UseNamedApiResourceField<MoveStatAffect, Move, MoveType>(x => x.Move);
            }
        }

        private sealed class MoveStatAffectSetsType : ObjectType<MoveStatAffectSets>
        {
            protected override void Configure(IObjectTypeDescriptor<MoveStatAffectSets> descriptor)
            {
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
