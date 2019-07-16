// <copyright file="PokemonColorType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal sealed class PokemonColorType : BaseNamedApiObjectType<PokemonColor>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<PokemonColor> descriptor)
        {
            descriptor.Description(@"Colors used for sorting pokémon in a pokédex. 
                The color listed in the Pokédex is usually the color most apparent or covering each Pokémon's body. 
                No orange category exists; Pokémon that are primarily orange are listed as red or brown.");
            descriptor.UseNamedApiResourceCollectionField<PokemonColor, PokemonSpecies, PokemonSpeciesType>(x => x.PokemonSpecies);
        }
    }
}
