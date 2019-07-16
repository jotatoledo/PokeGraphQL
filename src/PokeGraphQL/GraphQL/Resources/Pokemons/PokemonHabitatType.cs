// <copyright file="PokemonHabitatType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal sealed class PokemonHabitatType : BaseNamedApiObjectType<PokemonHabitat>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<PokemonHabitat> descriptor)
        {
            descriptor.Description(@"Habitats are generally different terrain pokémon can be found in 
                but can also be areas designated for rare or legendary pokémon.");
            descriptor.UseNamedApiResourceCollectionField<PokemonHabitat, PokemonSpecies, PokemonSpeciesType>(x => x.PokemonSpecies);
        }
    }
}
