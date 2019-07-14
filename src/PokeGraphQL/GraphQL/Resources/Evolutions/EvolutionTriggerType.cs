// <copyright file="EvolutionTriggerType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Evolutions
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Pokemons;

    internal sealed class EvolutionTriggerType : BaseNamedApiObjectType<EvolutionTrigger>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<EvolutionTrigger> descriptor)
        {
            descriptor.Description("Evolution triggers are the events and conditions that cause a pokémon to evolve.");
            descriptor.Field(x => x.PokemonSpecies)
                .Description("A list of pokémon species that result from this evolution trigger.")
                .Type<ListType<PokemonSpeciesType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<PokemonResolver>();
                    var resourceTasks = ctx.Parent<EvolutionTrigger>()
                        .PokemonSpecies
                        .Select(species => resolver.GetPokemonSpeciesAsync(species.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
        }
    }
}
