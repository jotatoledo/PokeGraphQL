// <copyright file="PokemonShapeType.cs" company="PokeGraphQL.Net">
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

    internal sealed class PokemonShapeType : BaseNamedApiObjectType<PokemonShape>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<PokemonShape> descriptor)
        {
            descriptor.Description("Shapes used for sorting pokémon in a pokédex.");
            descriptor.Field(x => x.AwesomeNames)
                .Description("The \"scientific\" name of this pokémon shape listed in different languages.")
                .Type<ListType<AwesomeNameType>>();
            descriptor.Field(x => x.Species)
                .Description("A list of the pokémon species that have this shape.")
                .Type<ListType<PokemonSpeciesType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<PokemonResolver>();
                    var resourceTasks = ctx.Parent<PokemonShape>()
                        .Species
                        .Select(species => resolver.GetPokemonSpeciesAsync(species.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
        }

        private sealed class AwesomeNameType : ObjectType<AwesomeName>
        {
            protected override void Configure(IObjectTypeDescriptor<AwesomeName> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.Name)
                    .Description("The localized \"scientific\" name for an API resource in a specific language.");

                // TODO implement ignored field
                descriptor.Field(x => x.Language)
                    .Description("The language this \"scientific\" name is in.")
                    .Ignore();
            }
        }
    }
}
