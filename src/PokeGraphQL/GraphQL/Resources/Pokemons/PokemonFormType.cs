// <copyright file="PokemonFormType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Games;

    internal sealed class PokemonFormType : BaseNamedApiObjectType<PokemonForm>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<PokemonForm> descriptor)
        {
            descriptor.Description(@"Some pokémon have the ability to take on different forms. 
                At times, these differences are purely cosmetic and have no bearing on the difference in the Pokémon's stats from another; 
                however, several Pokémon differ in stats (other than HP), type, and Ability depending on their form.");
            descriptor.Ignore(x => x.FormNames);
            descriptor.Ignore(x => x.Sprites);
            descriptor.Field(x => x.Order)
                .Description(@"The order in which forms should be sorted within all forms. 
                    Multiple forms may have equal order, in which case they should fall back on sorting by name.");
            descriptor.Field(x => x.FormOrder)
                .Description("The order in which forms should be sorted within a species' forms.");
            descriptor.Field(x => x.IsDefault)
                .Description("True for exactly one form used as the default for each pokémon.");
            descriptor.Field(x => x.IsBattleOnly)
                .Description("Whether or not this form can only happen during battle.");
            descriptor.Field(x => x.IsMega)
                .Description("Whether or not this form requires mega evolution.");
            descriptor.Field(x => x.FormName)
                .Description("The name of this form.");
            descriptor.UseNamedApiResourceField<PokemonForm, Pokemon, PokemonType>(x => x.Pokemon);
            descriptor.UseNamedApiResourceField<PokemonForm, VersionGroup, VersionGroupType>(x => x.VersionGroup);
        }
    }
}
