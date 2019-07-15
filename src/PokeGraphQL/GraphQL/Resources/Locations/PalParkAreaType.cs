// <copyright file="PalParkAreaType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Locations
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Pokemons;

    internal sealed class PalParkAreaType : BaseNamedApiObjectType<PalParkArea>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<PalParkArea> descriptor)
        {
            descriptor.Description("Areas used for grouping pokémon encounters in Pal Park. They're like habitats that are specific to Pal Park.");
            descriptor.Field(x => x.PokemonEncounters)
                .Description("A list of pokémon encountered in thi pal park area along with details")
                .Type<ListType<PalParkEncounterSpeciesType>>();
        }

        private sealed class PalParkEncounterSpeciesType : ObjectType<PalParkEncounterSpecies>
        {
            protected override void Configure(IObjectTypeDescriptor<PalParkEncounterSpecies> descriptor)
            {
                descriptor.Field(x => x.BaseScore)
                    .Description("The base score given to the player when this pokémon is caught during a pal park run.");
                descriptor.Field(x => x.Rate)
                    .Description("The base rate for encountering this pokémon in this pal park area.");
                descriptor.Field(x => x.PokemonSpecies)
                    .Description("The pokémon species being encountered.")
                    .Type<PokemonSpeciesType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetPokemonSpeciesAsync(ctx.Parent<PalParkEncounterSpecies>().PokemonSpecies.Name, token));
            }
        }
    }
}
