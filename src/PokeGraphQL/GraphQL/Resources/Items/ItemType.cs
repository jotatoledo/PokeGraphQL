// <copyright file="ItemType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Items
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Common;
    using PokeGraphQL.GraphQL.Resources.Evolutions;
    using PokeGraphQL.GraphQL.Resources.Pokemons;
    using PokemonType = PokeGraphQL.GraphQL.Resources.Pokemons.PokemonType;

    internal sealed class ItemType : BaseNamedApiObjectType<Item>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Item> descriptor)
        {
            descriptor.Description(@"An item is an object in the games which the player can pick up, keep in their bag, and use in some manner. 
                They have various uses, including healing, powering up, helping catch Pokémon, or to access a new area.");
            descriptor.Field(x => x.Cost)
                .Description("The price of this item in stores.");
            descriptor.Field(x => x.FlingPower)
                .Description("The power of the move Fling when used with this item.");
            descriptor.Field(x => x.FlingEffect)
                .Description("The effect of the move Fling when used with this item.")
                .Type<ItemFlingEffectType>()
                .Resolver((ctx, token) => ctx.Service<ItemResolver>().GetFlingEffectAsync(ctx.Parent<Item>().FlingEffect.Name, token));
            descriptor.Field(x => x.Attributes)
                .Description("A list of attributes this item has.")
                .Type<ListType<ItemAttributeType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<ItemResolver>();
                    var resourceTasks = ctx.Parent<Item>()
                        .Attributes
                        .Select(attribute => resolver.GetAttributeAsync(attribute.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
            descriptor.Field(x => x.Category)
                .Description("The category of items this item falls into.")
                .Type<ItemCategoryType>()
                .Resolver((ctx, token) => ctx.Service<ItemResolver>().GetCategoryAsync(ctx.Parent<Item>().Category.Name, token));
            descriptor.Field(x => x.HeldByPokemon)
                .Description("A list of pokémon that might be found in the wild holding this item.")
                .Type<ListType<ItemHolderPokemonType>>();
            descriptor.Field(x => x.GameIndices)
                .Description("A list of game indices relevent to this item by generation.")
                .Type<ListType<GenerationGameIndexType>>();
            descriptor.Field(x => x.BabyTriggerFor)
                .Description("An evolution chain this item requires to produce a bay during mating.")
                .Type<EvolutionChainType>()
                .Resolver((ctx, token) => ctx.Service<EvolutionResolver>().GetEvolutionChainAsync(Convert.ToInt32(ctx.Parent<Item>().BabyTriggerFor.Url), token));
            descriptor.Field(x => x.EffectEntries)
                .Description("The effect of this ability listed in different languages.")
                .Type<ListType<VerboseEffectType>>();
            descriptor.Field(x => x.FlavorGroupTextEntries)
                .Description("The flavor text of this ability listed in different languages.")
                .Type<ListType<VersionGroupFlavorTextType>>();
        }

        private sealed class ItemHolderPokemonType : ObjectType<ItemHolderPokemon>
        {
            /// <inheritdoc/>
            protected override void Configure(IObjectTypeDescriptor<ItemHolderPokemon> descriptor)
            {
                descriptor.Field(x => x.Pokemon)
                    .Description("The pokemon who might be holding the item.")
                    .Type<PokemonType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetPokemonAsync(ctx.Parent<ItemHolderPokemon>().Pokemon.Name, token));
                descriptor.Field(x => x.VersionDetails)
                    .Description("Details on chance of the pokemon having the item based on version.")
                    .Type<ListType<HeldItemVersionDetailsType>>();
            }
        }
    }
}
