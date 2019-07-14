// <copyright file="VersionGameIndexType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources
{
    using HotChocolate.Types;
    using PokeAPI;
    using PokeGraphQL.GraphQL.Resources.Games;

    internal sealed class VersionGameIndexType : ObjectType<VersionGameIndex>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<VersionGameIndex> descriptor)
        {
            descriptor.FixStructType();
            descriptor.Field(x => x.GameIndex)
                .Description("The internal id of an API resource within game data.");
            descriptor.Field(x => x.Version)
                .Description("The version relevent to this game index.")
                .Type<VersionType>()
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetVersionAsync(ctx.Parent<VersionGameIndex>().Version.Name, token));
        }
    }
}
