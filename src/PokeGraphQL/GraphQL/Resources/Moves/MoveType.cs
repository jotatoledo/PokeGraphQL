namespace PokeGraphQL.GraphQL.Resources.Moves
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using DotNetFunctional.Maybe;
    using HotChocolate.Types;
    using PokeAPI;
    using PokeGraphQL.GraphQL.Resources.Contests;
    using PokeGraphQL.GraphQL.Resources.Games;
    using PokeGraphQL.GraphQL.Resources.Pokemons;
    using MonadMaybe = DotNetFunctional.Maybe.Maybe;

    internal sealed class MoveType : BaseNamedApiObjectType<Move>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Move> descriptor)
        {
            // TODO implement ignored fields
            descriptor.Description(@"Moves are the skills of pokémon in battle. 
                In battle, a Pokémon uses one move each turn. 
                Some moves (including those learned by Hidden Machine) can be used outside 
                of battle as well, usually for the purpose of removing obstacles or exploring new areas.");
            descriptor.Ignore(x => x.FlavorTextEntries);
            descriptor.Field(x => x.Machines)
                .Ignore();
            descriptor.Field(x => x.Accuracy)
                .Description("The percent value of how likely this move is to be successful.");
            descriptor.Field(x => x.EffectChance)
                .Description("The percent value of how likely it is this moves effect will happen.");
            descriptor.Field(x => x.PP)
                .Name("pp")
                .Description("Power points. The number of times this move can be used.");
            descriptor.Field(x => x.Priority)
                .Description("A value between -8 and 8. Sets the order in which moves are executed during battle.");
            descriptor.Field(x => x.Power)
                .Description("The base power of this move with a value of 0 if it does not have a base power");
            descriptor.Field(x => x.ComboSets)
                .Description("A detail of normal and super contest combos that require this move.")
                .Type<ContestComboSetType>();
            descriptor.Field(x => x.ContestType)
                .Description("The type of appeal this move gives a pokémon when used in a contest.")
                .Type<ContestTypeType>()
                .Resolver((ctx, token) => ctx.Service<ContestResolver>().GetContestTypeAsync(ctx.Parent<Move>().ContestType.Name, token));
            descriptor.Field(x => x.ContestEffect)
                .Description("The effect the move has when used in a contest.")
                .Type<ContestEffectType>()
                .Resolver((ctx, token) => ctx.Service<ContestResolver>().GetContestEffectAsync(Convert.ToInt32(ctx.Parent<Move>().ContestEffect.Url.LastSegment()), token));
            descriptor.Field(x => x.DamageClass)
                .Description("The type of damage the move inflicts on the target, e.g. physical.")
                .Type<MoveDamageClassType>()
                .Resolver((ctx, token) => ctx.Service<MoveResolver>().GetMoveDamageClassAsync(ctx.Parent<Move>().DamageClass.Name, token));
            descriptor.Field(x => x.Effects)
                .Description("The effect of this move listed in different languages.")
                .Ignore();
            descriptor.Field(x => x.EffectChanges)
                .Description("The list of previous effects this move has had across version groups of the games.")
                .Ignore();
            descriptor.Field(x => x.Generation)
                .Description("The generation in which this move was introduced.")
                .Type<GenerationType>()
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetGenerationAsync(ctx.Parent<Move>().Generation.Name, token));
            descriptor.Field(x => x.Meta)
                .Description("Meta data about this move.")
                .Type<MoveMetaDataType>();
            descriptor.Field(x => x.PastValues)
                .Description("A list of move resource value changes across ersion groups of the game.")
                .Type<ListType<PastMoveStatValueType>>();
            descriptor.Field(x => x.StatChanges)
                .Description("A list of stats this moves effects and how much it effects them.")
                .Type<ListType<MoveStatChangeType>>();
            descriptor.Field(x => x.SuperContestEffect)
                .Description("The effect the move has when used in a super contest.")
                .Type<SuperContestEffectType>()
                .Resolver((ctx, token) => ctx.Service<ContestResolver>().GetSuperContestEffectAsync(Convert.ToInt32(ctx.Parent<Move>().SuperContestEffect.Url.LastSegment()), token));
            descriptor.Field(x => x.Target)
                .Description("The type of target that will recieve the effects of the attack.")
                .Type<MoveTargetType>()
                .Resolver((ctx, token) => ctx.Service<MoveResolver>().GetMoveTargetAsync(ctx.Parent<Move>().Target.Name, token));
            descriptor.Field(x => x.Type)
                .Description("The elemental type of this move.")
                .Type<TypePropertyType>()
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetTypeAsync(ctx.Parent<Move>().Type.Name, token));
        }

        private sealed class PastMoveStatValueType : ObjectType<PastMoveStatValue>
        {
            protected override void Configure(IObjectTypeDescriptor<PastMoveStatValue> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.Accuracy)
                    .Description("The percent value of how likely this move is to be successful.");
                descriptor.Field(x => x.EffectChance)
                    .Description("The percent value of how likely it is this moves effect will take effect.");
                descriptor.Field(x => x.Power)
                    .Description("The base power of this move with a value of 0 if it does not have a base power.");
                descriptor.Field(x => x.PP)
                    .Name("pp")
                    .Description("Power points. The number of times this move can be used.");
                descriptor.Field(x => x.Effects)
                    .Description("The effect of this move listed in different languages.")
                    .Ignore();
                descriptor.Field(x => x.Type)
                    .Description("The elemental type of this move.")
                    .Type<TypePropertyType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetTypeAsync(ctx.Parent<PastMoveStatValue>().Type.Name, token));
                descriptor.Field(x => x.VersionGroup)
                    .Description("The version group in which these move stat values were in effect.")
                    .Type<VersionGroupType>()
                    .Resolver((ctx, token) => ctx.Service<GameResolver>().GetVersionGroupAsync(ctx.Parent<PastMoveStatValue>().VersionGroup.Name, token));
            }
        }

        private sealed class MoveStatChangeType : ObjectType<MoveStatChange>
        {
            protected override void Configure(IObjectTypeDescriptor<MoveStatChange> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.Change)
                    .Description("The amount of change.");
                descriptor.Field(x => x.Stat)
                    .Description("The stat being affected.")
                    .Type<StatType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetStatAsync(ctx.Parent<MoveStatChange>().Stat.Name, token));
            }
        }

        private sealed class ContestComboDetailType : ObjectType<ContestComboDetail>
        {
            protected override void Configure(IObjectTypeDescriptor<ContestComboDetail> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.UseBefore)
                    .Description("A list of moves to use before this move.")
                    .Type<ListType<MoveType>>()
                    .Resolver((ctx, token) =>
                    {
                        var resolver = ctx.Service<MoveResolver>();
                        var resourceTasks = MonadMaybe.Lift(ctx.Parent<ContestComboDetail>())
                            .Select(x => x.UseBefore)
                            .Select(moves => moves.Select(move => resolver.GetMoveAsync(move.Name, token)))
                            .Select(Task.WhenAll);
                        return resourceTasks.HasValue ? resourceTasks.Value : Task.FromResult<Move[]>(default);
                    });
                descriptor.Field(x => x.UseAfter)
                    .Description("A list of moves to use after this move.")
                    .Type<ListType<MoveType>>()
                    .Resolver((ctx, token) =>
                    {
                        var resolver = ctx.Service<MoveResolver>();
                        var resourceTasks = MonadMaybe.Lift(ctx.Parent<ContestComboDetail>())
                            .Select(x => x.UseAfter)
                            .Select(moves => moves.Select(move => resolver.GetMoveAsync(move.Name, token)))
                            .Select(Task.WhenAll);
                        return resourceTasks.HasValue ? resourceTasks.Value : Task.FromResult<Move[]>(default);
                    });
            }
        }

        private sealed class ContestComboSetType : ObjectType<ContestComboSet>
        {
            protected override void Configure(IObjectTypeDescriptor<ContestComboSet> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.Normal)
                    .Description("A detail of moves this move can be used before or after, granting additional appeal points in contests.")
                    .Type<ContestComboDetailType>();
                descriptor.Field(x => x.Super)
                    .Description("A detail of moves this move can be used before or after, granting additional appeal points in super contests.")
                    .Type<ContestComboDetailType>();
            }
        }

        private sealed class MoveMetaDataType : ObjectType<MoveMetadata>
        {
            /// <inheritdoc/>
            protected override void Configure(IObjectTypeDescriptor<MoveMetadata> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.Ailment)
                    .Description("	The status ailment this move inflicts on its target.")
                    .Type<MoveAilmentType>()
                    .Resolver((ctx, token) => ctx.Service<MoveResolver>().GetMoveAilmentAsync(ctx.Parent<MoveMetadata>().Ailment.Name, token));
                descriptor.Field(x => x.Category)
                    .Description("The category of move this move falls under, e.g. damage or ailment.")
                    .Type<MoveCategoryType>()
                    .Resolver((ctx, token) => ctx.Service<MoveResolver>().GetMoveCategoryAsync(ctx.Parent<MoveMetadata>().Category.Name, token));
                descriptor.Field(x => x.MinHits)
                    .Description("The minimum number of times this move hits. Null if it always only hits once.");
                descriptor.Field(x => x.MaxHits)
                    .Description("The maximum number of times this move hits. Null if it always only hits once.");
                descriptor.Field(x => x.MinTurns)
                    .Description("The minimum number of turns this move continues to take effect. Null if it always only lasts one turn.");
                descriptor.Field(x => x.MaxTurns)
                    .Description("The maximum number of turns this move continues to take effect. Null if it always only lasts one turn.");
                descriptor.Field(x => x.DrainRecoil)
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
