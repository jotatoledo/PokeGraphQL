// <copyright file="Class.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Items
{
    using HotChocolate.Types;
    using PokeAPI;
    using PokeGraphQL.GraphQL.Resources.Games;

    internal sealed class HeldItemVersionDetailsType : ObjectType<VersionDetails>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<VersionDetails> descriptor)
        {
            descriptor.Field(x => x.Rarity)
                .Description("The chance of the pokemon holding the item.");
            descriptor.Field(x => x.Version)
                .Description("The version the rarity applies.")
                .Type<VersionType>()
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetVersionAsync(ctx.Parent<VersionDetails>().Version.Name, token));
        }
    }
}
