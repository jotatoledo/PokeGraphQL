// <copyright file="PokemonEntryType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Games
{
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class PokemonEntryType : ObjectType<PokemonEntry>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<PokemonEntry> descriptor)
        {
            // TODO: implement ignored field
            descriptor.Field(x => x.EntryNumber)
                .Description("The index of this pokémon species entry within the pokédex.");
            descriptor.Field(x => x.Species)
                .Name("pokemonSpecies")
                .Description("The pokémon species being encountered.")
                .Ignore();
        }
    }
}
