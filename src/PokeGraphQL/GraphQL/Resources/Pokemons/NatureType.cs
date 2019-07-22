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
            descriptor.UseNamedApiResourceField<Nature, Stat, StatType>(x => x.DecreasedStat);
            descriptor.UseNamedApiResourceField<Nature, Stat, StatType>(x => x.IncreasedStat);
            descriptor.UseNamedApiResourceField<Nature, BerryFlavor, BerryFlavorType>(x => x.HatesFlavor);
            descriptor.UseNamedApiResourceField<Nature, BerryFlavor, BerryFlavorType>(x => x.LikesFlavor);
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
                descriptor.UseNamedApiResourceField<MoveBattleStylePreference, MoveBattleStyle, MoveBattleStyleType>(x => x.MoveBattleStyle);
            }
        }

        private sealed class NatureStatChangeType : ObjectType<NatureStatChange>
        {
            protected override void Configure(IObjectTypeDescriptor<NatureStatChange> descriptor)
            {
                descriptor.Field(x => x.MaxChange)
                    .Description("The amount of change.");
                descriptor.UseNamedApiResourceField<NatureStatChange, PokeathlonStat, PokeathlonStatType>(x => x.PokeathlonStat);
            }
        }
    }
}
