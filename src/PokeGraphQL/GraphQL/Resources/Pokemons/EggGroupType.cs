// <copyright file="EggGroupType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal sealed class EggGroupType : BaseNamedApiObjectType<EggGroup>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<EggGroup> descriptor)
        {
            descriptor.Description(@"Egg Groups are categories which determine which Pokémon are able to interbreed. 
                Pokémon may belong to either one or two Egg Groups.");
            descriptor.UseNamedApiResourceCollectionField<EggGroup, PokemonSpecies, PokemonSpeciesType>(x => x.PokemonSpecies);
        }
    }
}
