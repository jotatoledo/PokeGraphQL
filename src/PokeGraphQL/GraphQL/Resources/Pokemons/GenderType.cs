﻿// <copyright file="GenderType.cs" company="PokeGraphQL.Net">
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

    internal sealed class GenderType : BaseNamedApiObjectType<Gender>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Gender> descriptor)
        {
            descriptor.Description(@"Genders were introduced in Generation II for the purposes of breeding pokémon 
                but can also result in visual differences or even different evolutionary lines.");
            descriptor.Field(x => x.SpeciesDetails)
                .Description("A list of pokémon species that can be this gender and how likely it is that they will be.")
                .Type<ListType<PokemonSpeciesGenderType>>();
            descriptor.Field(x => x.RequiredForEvolution)
                .Description("A list of pokémon species that required this gender in order for a pokémon to evolve into them")
                .Type<ListType<PokemonSpeciesType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<PokemonResolver>();
                    var resourceTasks = ctx.Parent<Gender>()
                        .RequiredForEvolution
                        .Select(species => resolver.GetPokemonSpeciesAsync(species.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
        }

        private sealed class PokemonSpeciesGenderType : ObjectType<PokemonSpeciesGender>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonSpeciesGender> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.FamaleToMaleRate)
                    .Name("genderRate")
                    .Description("The chance of this Pokémon being female, in eighths; or -1 for genderless.");
                descriptor.Field(x => x.Species)
                    .Description("A pokemon species that can be the referenced gender")
                    .Type<PokemonSpeciesType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetPokemonSpeciesAsync(ctx.Parent<PokemonSpeciesGender>().Species.Name, token));
            }
        }
    }
}
