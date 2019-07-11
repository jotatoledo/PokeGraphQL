// <copyright file="GrowthRateType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;

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
            descriptor.Field(x => x.Species)
                .Description("	A list of pokémon species that gain levels at this growth rate.")
                .Type<ListType<PokemonSpeciesType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<PokemonResolver>();
                    var resourceTasks = ctx.Parent<GrowthRate>()
                        .Species
                        .Select(species => resolver.GetPokemonSpeciesAsync(species.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
        }

        private sealed class GrowthRateExperienceLevelType : ObjectType<GrowthRateExperienceLevel>
        {
            protected override void Configure(IObjectTypeDescriptor<GrowthRateExperienceLevel> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.Level)
                    .Description("The level gained.");
                descriptor.Field(x => x.Experience)
                    .Description("The amount of experience required to reach the referenced level.");
            }
        }
    }
}
