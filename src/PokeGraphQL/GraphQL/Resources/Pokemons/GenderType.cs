// <copyright file="GenderType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal sealed class GenderType : BaseNamedApiObjectType<Gender>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Gender> descriptor)
        {
            descriptor.Description(@"Genders were introduced in Generation II for the purposes of breeding pokémon 
                but can also result in visual differences or even different evolutionary lines.");
            descriptor.Field(x => x.PokemonSpeciesDetails)
                .Description("A list of pokémon species that can be this gender and how likely it is that they will be.")
                .Type<ListType<PokemonSpeciesGenderType>>();
            descriptor.UseNamedApiResourceCollectionField<Gender, PokemonSpecies, PokemonSpeciesType>(x => x.RequiredForEvolution);
        }

        private sealed class PokemonSpeciesGenderType : ObjectType<PokemonSpeciesGender>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonSpeciesGender> descriptor)
            {
                descriptor.Field(x => x.Rate)
                    .Name("genderRate")
                    .Description("The chance of this Pokémon being female, in eighths; or -1 for genderless.");
                descriptor.UseNamedApiResourceField<PokemonSpeciesGender, PokemonSpecies, PokemonSpeciesType>(x => x.PokemonSpecies);
            }
        }
    }
}
