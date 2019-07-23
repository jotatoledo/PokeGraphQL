// <copyright file="MoveType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Moves
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Common;
    using PokeGraphQL.GraphQL.Resources.Contests;
    using PokeGraphQL.GraphQL.Resources.Games;
    using PokeGraphQL.GraphQL.Resources.Pokemons;

    internal sealed class MoveType : BaseNamedApiObjectType<Move>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Move> descriptor)
        {
            descriptor.Description(@"Moves are the skills of pokémon in battle. 
                In battle, a Pokémon uses one move each turn. 
                Some moves (including those learned by Hidden Machine) can be used outside 
                of battle as well, usually for the purpose of removing obstacles or exploring new areas.");
            descriptor.Ignore(x => x.FlavorTextEntries);
            descriptor.Field(x => x.Accuracy)
                .Description("The percent value of how likely this move is to be successful.");
            descriptor.Field(x => x.EffectChance)
                .Description("The percent value of how likely it is this moves effect will happen.");
            descriptor.Field(x => x.Pp)
                .Description("Power points. The number of times this move can be used.");
            descriptor.Field(x => x.Priority)
                .Description("A value between -8 and 8. Sets the order in which moves are executed during battle.");
            descriptor.Field(x => x.Power)
                .Description("The base power of this move with a value of 0 if it does not have a base power");
            descriptor.Field(x => x.ContestCombos)
                .Description("A detail of normal and super contest combos that require this move.")
                .Type<ContestComboSetsType>();
            descriptor.UseNamedApiResourceField<Move, ContestType, ContestTypeType>(x => x.ContestType);
            descriptor.UseApiResourceField<Move, ContestEffect, ContestEffectType>(x => x.ContestEffect);
            descriptor.UseNamedApiResourceField<Move, MoveDamageClass, MoveDamageClassType>(x => x.DamageClass);
            descriptor.Field(x => x.EffectEntries)
                .Description("The effect of this move listed in different languages.")
                .Type<ListType<VerboseEffectType>>();
            descriptor.Field(x => x.EffectChanges)
                .Description("The list of previous effects this move has had across version groups of the games.")
                .Type<ListType<AbilityEffectChangeType>>();
            descriptor.UseNamedApiResourceField<Move, Generation, GenerationType>(x => x.Generation);
            descriptor.Field(x => x.Meta)
                .Description("Meta data about this move.")
                .Type<MoveMetaDataType>();
            descriptor.Field(x => x.PastValues)
                .Description("A list of move resource value changes across ersion groups of the game.")
                .Type<ListType<PastMoveStatValuesType>>();
            descriptor.Field(x => x.StatChanges)
                .Description("A list of stats this moves effects and how much it effects them.")
                .Type<ListType<MoveStatChangeType>>();
            descriptor.UseApiResourceField<Move, SuperContestEffect, SuperContestEffectType>(x => x.SuperContestEffect);
            descriptor.UseNamedApiResourceField<Move, MoveTarget, MoveTargetType>(x => x.Target);
            descriptor.UseNamedApiResourceField<Move, Type, TypePropertyType>(x => x.Type);
            descriptor.Field(x => x.Machines)
                .Type<ListType<MachineVersionDetailType>>();
        }

        private sealed class PastMoveStatValuesType : ObjectType<PastMoveStatValues>
        {
            protected override void Configure(IObjectTypeDescriptor<PastMoveStatValues> descriptor)
            {
                descriptor.Field(x => x.Accuracy)
                    .Description("The percent value of how likely this move is to be successful.");
                descriptor.Field(x => x.EffectChance)
                    .Description("The percent value of how likely it is this moves effect will take effect.");
                descriptor.Field(x => x.Power)
                    .Description("The base power of this move with a value of 0 if it does not have a base power.");
                descriptor.Field(x => x.Pp)
                    .Description("Power points. The number of times this move can be used.");
                descriptor.Field(x => x.EffectEntries)
                    .Description("The effect of this move listed in different languages.")
                    .Ignore();
                descriptor.UseNamedApiResourceField<PastMoveStatValues, Type, TypePropertyType>(x => x.Type);
                descriptor.UseNamedApiResourceField<PastMoveStatValues, VersionGroup, VersionGroupType>(x => x.VersionGroup);
            }
        }

        private sealed class MoveStatChangeType : ObjectType<MoveStatChange>
        {
            protected override void Configure(IObjectTypeDescriptor<MoveStatChange> descriptor)
            {
                descriptor.Field(x => x.Change)
                    .Description("The amount of change.");
                descriptor.UseNamedApiResourceField<MoveStatChange, Stat, StatType>(x => x.Stat);
            }
        }

        private sealed class ContestComboDetailType : ObjectType<ContestComboDetail>
        {
            protected override void Configure(IObjectTypeDescriptor<ContestComboDetail> descriptor)
            {
                descriptor.UseNullableNamedApiResourceCollectionField<ContestComboDetail, Move, MoveType>(x => x.UseBefore)
                    .Description("A list of moves to use before this move.");
                descriptor.UseNullableNamedApiResourceCollectionField<ContestComboDetail, Move, MoveType>(x => x.UseAfter)
                    .Description("A list of moves to use after this move.");
            }
        }

        private sealed class ContestComboSetsType : ObjectType<ContestComboSets>
        {
            protected override void Configure(IObjectTypeDescriptor<ContestComboSets> descriptor)
            {
                descriptor.Field(x => x.Normal)
                    .Description("A detail of moves this move can be used before or after, granting additional appeal points in contests.")
                    .Type<ContestComboDetailType>();
                descriptor.Field(x => x.Super)
                    .Description("A detail of moves this move can be used before or after, granting additional appeal points in super contests.")
                    .Type<ContestComboDetailType>();
            }
        }

        private sealed class MoveMetaDataType : ObjectType<MoveMetaData>
        {
            /// <inheritdoc/>
            protected override void Configure(IObjectTypeDescriptor<MoveMetaData> descriptor)
            {
                descriptor.UseNamedApiResourceField<MoveMetaData, MoveAilment, MoveAilmentType>(x => x.Ailment);
                descriptor.UseNamedApiResourceField<MoveMetaData, MoveCategory, MoveCategoryType>(x => x.Category)
                    .Description("The category of move this move falls under, e.g. damage or ailment.");
                descriptor.Field(x => x.MinHits)
                    .Description("The minimum number of times this move hits. Null if it always only hits once.");
                descriptor.Field(x => x.MaxHits)
                    .Description("The maximum number of times this move hits. Null if it always only hits once.");
                descriptor.Field(x => x.MinTurns)
                    .Description("The minimum number of turns this move continues to take effect. Null if it always only lasts one turn.");
                descriptor.Field(x => x.MaxTurns)
                    .Description("The maximum number of turns this move continues to take effect. Null if it always only lasts one turn.");
                descriptor.Field(x => x.Drain)
                    .Description("HP drain (if positive) or Recoil damage (if negative), in percent of damage done.");
                descriptor.Field(x => x.Healing)
                    .Description("The amount of hp gained by the attacking pokémon, in percent of it's maximum HP.");
                descriptor.Field(x => x.CritRate)
                    .Description("Critical hit rate bonus.");
                descriptor.Field(x => x.AilmentChance)
                    .Description("The likelyhood this attack will cause an ailment.");
                descriptor.Field(x => x.FlinchChance)
                    .Description("The likelyhood this attack will cause the target pokémon to flinch.");
                descriptor.Field(x => x.StatChance)
                    .Description("The likelyhood this attack will cause a stat change in the target pokémon.");
            }
        }
    }
}
