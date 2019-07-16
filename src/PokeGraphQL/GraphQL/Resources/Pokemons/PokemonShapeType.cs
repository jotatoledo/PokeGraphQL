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
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Languages;

    internal sealed class PokemonShapeType : BaseNamedApiObjectType<PokemonShape>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<PokemonShape> descriptor)
        {
            descriptor.Description("Shapes used for sorting pokémon in a pokédex.");
            descriptor.Field(x => x.AwesomeNames)
                .Description("The \"scientific\" name of this pokémon shape listed in different languages.")
                .Type<ListType<AwesomeNameType>>();

            // TODO refactor once type in upstream is changed to List<NamedApiResource<PokemonSpecies>>
            descriptor.Field(x => x.PokemonSpecies)
                .Description("A list of the pokémon species that have this shape.")
                .Type<ListType<PokemonSpeciesType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<PokemonResolver>();
                    var resourceTasks = ctx.Parent<PokemonShape>()
                        .PokemonSpecies
                        .Select(species => resolver.GetPokemonSpeciesAsync(species.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
        }

        private sealed class AwesomeNameType : ObjectType<AwesomeNames>
        {
            protected override void Configure(IObjectTypeDescriptor<AwesomeNames> descriptor)
            {
                descriptor.Field(x => x.AwesomeName)
                    .Description("The localized \"scientific\" name for an API resource in a specific language.");
                descriptor.UseNamedApiResourceField<AwesomeNames, Language, LanguageType>(x => x.Language);
            }
        }
    }
}
