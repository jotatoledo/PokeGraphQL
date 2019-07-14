// <copyright file="CharacteristicType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal sealed class CharacteristicType : BaseApiObjectType<Characteristic>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Characteristic> descriptor)
        {
            descriptor.Description(@"Characteristics indicate which stat contains a Pokémon's highest IV.
                A Pokémon's Characteristic is determined by the remainder of its highest IV divided by 5 (gene_modulo).");
            descriptor.Field(x => x.GeneModulo)
                .Description("The remainder of the highest stat/IV divided by 5.");
            descriptor.Field(x => x.PossibleValues)
                .Description("The possible values of the highest stat that would result in a pokémon recieving this characteristic when divided by 5.")
                .Type<ListType<IntType>>();

            // TODO missing properties, report in upstream
            // See https://pokeapi.co/api/v2/characteristic/1
            //descriptor.Field(x => x.HighestStat)
            //    .Description("The highest stat of this characteristic")
            //    .Type<StatType>()
            //    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetStatAsync(ctx.Parent<Characteristic>().HighestStat.Name, token));
            //descriptor.Ignore(x => x.Descriptions);
        }
    }
}
