// <copyright file="EvolutionTriggerType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Evolutions
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Pokemons;

    internal sealed class EvolutionTriggerType : BaseNamedApiObjectType<EvolutionTrigger>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<EvolutionTrigger> descriptor)
        {
            descriptor.Description("Evolution triggers are the events and conditions that cause a pokémon to evolve.");
            descriptor.UseNamedApiResourceCollectionField<EvolutionTrigger, PokemonSpecies, PokemonSpeciesType>(x => x.PokemonSpecies);
        }
    }
}
