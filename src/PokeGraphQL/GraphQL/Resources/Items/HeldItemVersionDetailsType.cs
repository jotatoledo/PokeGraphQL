﻿// <copyright file="HeldItemVersionDetailsType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Items
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Games;

    internal sealed class HeldItemVersionDetailsType : ObjectType<ItemHolderPokemonVersionDetail>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<ItemHolderPokemonVersionDetail> descriptor)
        {
            descriptor.Field(x => x.Rarity)
                .Description("The chance of the pokemon holding the item.");
            descriptor.UseNamedApiResourceField<ItemHolderPokemonVersionDetail, Version, VersionType>(x => x.Version);
        }
    }
}
