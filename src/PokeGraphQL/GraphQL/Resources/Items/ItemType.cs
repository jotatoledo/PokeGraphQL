// <copyright file="ItemType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Items
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Common;
    using PokeGraphQL.GraphQL.Resources.Evolutions;
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
            descriptor.UseNullableNamedApiResourceField<Item, ItemFlingEffect, ItemFlingEffectType>(x => x.FlingEffect);
            descriptor.UseNamedApiResourceCollectionField<Item, ItemAttribute, ItemAttributeType>(x => x.Attributes);
            descriptor.UseNamedApiResourceField<Item, ItemCategory, ItemCategoryType>(x => x.Category);
            descriptor.Field(x => x.HeldByPokemon)
                .Description("A list of pokémon that might be found in the wild holding this item.")
                .Type<ListType<ItemHolderPokemonType>>();
            descriptor.Field(x => x.GameIndices)
                .Description("A list of game indices relevent to this item by generation.")
                .Type<ListType<GenerationGameIndexType>>();
            descriptor.UseNullableApiResourceField<Item, EvolutionChain, EvolutionChainType>(x => x.BabyTriggerFor);
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
                descriptor.UseNamedApiResourceField<ItemHolderPokemon, Pokemon, PokemonType>(x => x.Pokemon);
                descriptor.Field(x => x.VersionDetails)
                    .Description("Details on chance of the pokemon having the item based on version.")
                    .Type<ListType<HeldItemVersionDetailsType>>();
            }
        }
    }
}
