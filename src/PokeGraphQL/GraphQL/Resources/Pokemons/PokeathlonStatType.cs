// <copyright file="PokeathlonStatType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal sealed class PokeathlonStatType : BaseNamedApiObjectType<PokeathlonStat>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<PokeathlonStat> descriptor)
        {
            descriptor.Description(@"Pokéathlon Stats are different attributes of a pokémon's performance in pokeathlons. 
                In Pokéathlons, competitions happen on different courses; one for each of the different pokeathlon stats.");
            descriptor.Field(x => x.AffectingNatures)
                .Description("A detail of natures which affect this pokéathlon stat positively or negatively.")
                .Type<NaturePokeathlonStatAffectSetType>();
        }

        private sealed class NaturePokeathlonStatAffectType : ObjectType<NaturePokeathlonStatAffect>
        {
            protected override void Configure(IObjectTypeDescriptor<NaturePokeathlonStatAffect> descriptor)
            {
                descriptor.Field(x => x.MaxChange)
                    .Description("The maximum amount of change to the referenced pokéathlon stat.");
                descriptor.Field(x => x.Nature)
                    .Description("The nature causing the change.")
                    .Type<NatureType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetNatureAsync(ctx.Parent<NaturePokeathlonStatAffect>().Nature.Name, token));
            }
        }

        private sealed class NaturePokeathlonStatAffectSetType : ObjectType<NaturePokeathlonStatAffectSets>
        {
            protected override void Configure(IObjectTypeDescriptor<NaturePokeathlonStatAffectSets> descriptor)
            {
                descriptor.Field(x => x.Increase)
                    .Description("A list of natures and how they change the referenced pokéathlon stat.")
                    .Type<ListType<NaturePokeathlonStatAffectType>>();
                descriptor.Field(x => x.Decrease)
                    .Description("A list of natures and how they change the referenced pokéathlon stat.")
                    .Type<ListType<NaturePokeathlonStatAffectType>>();
            }
        }
    }
}
