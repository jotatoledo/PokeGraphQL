// <copyright file="GrowthRateType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal sealed class GrowthRateType : BaseNamedApiObjectType<GrowthRate>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<GrowthRate> descriptor)
        {
            descriptor.Description("Growth rates are the speed with which pokémon gain levels through experience.");
            descriptor.Ignore(x => x.Descriptions);
            descriptor.Field(x => x.Formula)
                .Description("The formula used to calculate the rate at which the pokémon species gains level.")
                .Type<NonNullType<StringType>>();
            descriptor.Field(x => x.Levels)
                .Description("A list of levels and the amount of experienced needed to atain them based on this growth rate.")
                .Type<ListType<GrowthRateExperienceLevelType>>();
            descriptor.UseNamedApiResourceCollectionField<GrowthRate, PokemonSpecies, PokemonSpeciesType>(x => x.PokemonSpecies);
        }

        private sealed class GrowthRateExperienceLevelType : ObjectType<GrowthRateExperienceLevel>
        {
            protected override void Configure(IObjectTypeDescriptor<GrowthRateExperienceLevel> descriptor)
            {
                descriptor.Field(x => x.Level)
                    .Description("The level gained.");
                descriptor.Field(x => x.Experience)
                    .Description("The amount of experience required to reach the referenced level.");
            }
        }
    }
}
