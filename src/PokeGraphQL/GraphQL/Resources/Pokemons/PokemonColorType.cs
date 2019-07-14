// <copyright file="PokemonColorType.cs" company="PokeGraphQL.Net">
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

    internal sealed class PokemonColorType : BaseNamedApiObjectType<PokemonColour>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<PokemonColour> descriptor)
        {
            descriptor.Description(@"Colors used for sorting pokémon in a pokédex. 
                The color listed in the Pokédex is usually the color most apparent or covering each Pokémon's body. 
                No orange category exists; Pokémon that are primarily orange are listed as red or brown.");
            descriptor.Field(x => x.Species)
                .Description("A list of the pokémon species that have this color.")
                .Type<ListType<PokemonSpeciesType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<PokemonResolver>();
                    var resourceTasks = ctx.Parent<PokemonColour>()
                        .Species
                        .Select(species => resolver.GetPokemonSpeciesAsync(species.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
        }
    }
}
