// <copyright file="EggGroupType.cs" company="PokeGraphQL.Net">
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
    using PokeApiNet.Models;

    internal sealed class EggGroupType : BaseNamedApiObjectType<EggGroup>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<EggGroup> descriptor)
        {
            descriptor.Description(@"Egg Groups are categories which determine which Pokémon are able to interbreed. 
                Pokémon may belong to either one or two Egg Groups.");
            descriptor.Field(x => x.PokemonSpecies)
                .Description("A list of all pokémon species that are members of this egg group.")
                .Type<ListType<PokemonSpeciesType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<PokemonResolver>();
                    var resourceTasks = ctx.Parent<EggGroup>()
                        .PokemonSpecies
                        .Select(species => resolver.GetPokemonSpeciesAsync(species.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
        }
    }
}
