// <copyright file="NatureType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using HotChocolate.Types;
    using PokeAPI;
    using PokeGraphQL.GraphQL.Resources.Berries;

    internal sealed class NatureType : BaseNamedApiObjectType<Nature>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Nature> descriptor)
        {
            descriptor.Description("Natures influence how a pokémon's stats grow.");
            descriptor.Field(x => x.DecreasedStat)
                .Description("The stat decreased by 10% in pokémon with this nature.")
                .Type<StatType>()
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetStatAsync(ctx.Parent<Nature>().DecreasedStat.Name, token));
            descriptor.Field(x => x.IncreasedStat)
                .Description("The stat increased by 10% in pokémon with this nature.")
                .Type<StatType>()
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetStatAsync(ctx.Parent<Nature>().IncreasedStat.Name, token));
            descriptor.Field(x => x.HatesFlavor)
                .Description("The flavor hated by pokémon with this nature.")
                .Type<BerryFlavorType>()
                .Resolver((ctx, token) => ctx.Service<BerryResolver>().GetBerryFlavorAsync(ctx.Parent<Nature>().HatesFlavor.Name, token));
            descriptor.Field(x => x.LikesFlavor)
                .Description("The flavor liked by pokémon with this nature.")
                .Type<BerryFlavorType>()
                .Resolver((ctx, token) => ctx.Service<BerryResolver>().GetBerryFlavorAsync(ctx.Parent<Nature>().LikesFlavor.Name, token));
            descriptor.Field(x => x.PokeathlonStatChanges)
                .Description("A list of pokéathlon stats this nature effects and how much it effects them.")
                .Type<ListType<NatureStatChangeType>>();
            descriptor.Field(x => x.BattleStylePreferences)
                .Description("A list of battle styles and how likely a pokémon with this nature is to use them in the Battle Palace or Battle Tent.")
                .Type<ListType<MoveBattleStylePreferenceType>>();
        }

        private sealed class MoveBattleStylePreferenceType : ObjectType<MoveBattleStylePreference>
        {
            protected override void Configure(IObjectTypeDescriptor<MoveBattleStylePreference> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.LowHPPreference)
                    .Description("Chance of using the move, in percent, if HP is under one half.");
                descriptor.Field(x => x.HighHPPrefernece)
                    .Name("highHpPreference")
                    .Description("Chance of using the move, in percent, if HP is over one half.");

                // TODO implement ignored field
                descriptor.Field(x => x.BattleStyle)
                    .Description("The move battle style.")
                    .Ignore();
            }
        }

        private sealed class NatureStatChangeType : ObjectType<NatureStatChange>
        {
            protected override void Configure(IObjectTypeDescriptor<NatureStatChange> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.Change)
                    .Description("The amount of change.");
                descriptor.Field(x => x.Stat)
                    .Description("The stat being affected.")
                    .Type<StatType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetStatAsync(ctx.Parent<NatureStatChange>().Stat.Name, token));
            }
        }
    }
}
