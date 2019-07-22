// <copyright file="PokemonShapeType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
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
            descriptor.UseNamedApiResourceCollectionField<PokemonShape, PokemonSpecies, PokemonSpeciesType>(x => x.PokemonSpecies);
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
