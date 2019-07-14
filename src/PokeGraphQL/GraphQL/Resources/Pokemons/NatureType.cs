// <copyright file="NatureType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Berries;
    using PokeGraphQL.GraphQL.Resources.Moves;

    internal sealed class NatureType : BaseNamedApiObjectType<Nature>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Nature> descriptor)
        {
            descriptor.Description("Natures influence how a pokémon's stats grow.");
            
            // TODO fix typo in upstream
            descriptor.Field(x => x.DescreasedStat)
                .Name("decreasedStat")
                .Description("The stat decreased by 10% in pokémon with this nature.")
                .Type<StatType>()
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetStatAsync(ctx.Parent<Nature>().DescreasedStat.Name, token));
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
            descriptor.Field(x => x.MoveBattleStylePreferences)
                .Description("A list of battle styles and how likely a pokémon with this nature is to use them in the Battle Palace or Battle Tent.")
                .Type<ListType<MoveBattleStylePreferenceType>>();
        }

        private sealed class MoveBattleStylePreferenceType : ObjectType<MoveBattleStylePreference>
        {
            protected override void Configure(IObjectTypeDescriptor<MoveBattleStylePreference> descriptor)
            {
                descriptor.Field(x => x.LowHpPreference)
                    .Description("Chance of using the move, in percent, if HP is under one half.");
                descriptor.Field(x => x.HighHpPreference)
                    .Name("highHpPreference")
                    .Description("Chance of using the move, in percent, if HP is over one half.");
                descriptor.Field(x => x.MoveBattleStyle)
                    .Description("The move battle style.")
                    .Type<MoveBattleStyleType>()
                    .Resolver((ctx, token) => ctx.Service<MoveResolver>().GetMoveBattleStyleAsync(ctx.Parent<MoveBattleStylePreference>().MoveBattleStyle.Name, token));
            }
        }

        private sealed class NatureStatChangeType : ObjectType<NatureStatChange>
        {
            protected override void Configure(IObjectTypeDescriptor<NatureStatChange> descriptor)
            {
                descriptor.Field(x => x.MaxChange)
                    .Description("The amount of change.");

                // TODO type should be changed in upstream to NamedApiResource<PokeAthlonStat>
                // See https://pokeapi.co/api/v2/nature/1
                descriptor.Field(x => x.PokeathlonStat)
                    .Description("The stat being affected.")
                    .Type<PokeathlonStatType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetPokeathlonStatAsync(ctx.Parent<NatureStatChange>().PokeathlonStat.Name, token));
            }
        }
    }
}
