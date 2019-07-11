// <copyright file="AbilityType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using HotChocolate.Types;
    using PokeAPI;
    using PokeGraphQL.GraphQL.Resources.Games;

    internal sealed class AbilityType : BaseNamedApiObjectType<Ability>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Ability> descriptor)
        {
            // TODO implement ignored fields
            descriptor.Description(@"Abilities provide passive effects for pokémon in battle or in the overworld.
                Pokémon have mutiple possible abilities but can have only one ability at a time.");
            descriptor.Field(x => x.IsMainSeries)
                .Description("Whether or not this ability originated in the main series of the video games.");
            descriptor.Field(x => x.Generation)
                .Description("The generation this ability originated in.")
                .Type<GenerationType>()
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetGenerationAsync(ctx.Parent<Ability>().Generation.Name, token));
            descriptor.Field(x => x.Effects)
                .Description("The effect of this ability listed in different languages.")
                .Ignore();
            descriptor.Field(x => x.EffectChanges)
                .Description("The list of previous effects this ability has had across version groups.")
                .Ignore();
            descriptor.Field(x => x.FlavorTexts)
                .Description("The flavor text of this ability listed in different languages")
                .Ignore();
            descriptor.Field(x => x.Pokemon)
                .Description("A list of pokémon that could potentially have this ability.")
                .Type<ListType<AbilityPokemonType>>();
        }

        private sealed class AbilityPokemonType : ObjectType<AbilityPokemon>
        {
            protected override void Configure(IObjectTypeDescriptor<AbilityPokemon> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.IsHidden)
                    .Description("Whether or not this a hidden ability for the referenced pokémon.");
                descriptor.Field(x => x.Slot)
                    .Description("Pokémon have 3 ability 'slots' which hold references to possible abilities they could have. This is the slot of this ability for the referenced pokémon.");
                descriptor.Field(x => x.Pokemon)
                    .Description("The pokémon this ability could belong to.")
                    .Type<PokemonType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetPokemonAsync(ctx.Parent<AbilityPokemon>().Pokemon.Name, token));
            }
        }
    }
}
