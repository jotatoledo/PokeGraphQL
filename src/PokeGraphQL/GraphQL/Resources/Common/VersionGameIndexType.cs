// <copyright file="VersionGameIndexType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Common
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Games;

    internal sealed class VersionGameIndexType : ObjectType<VersionGameIndex>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<VersionGameIndex> descriptor)
        {
            descriptor.Field(x => x.GameIndex)
                .Description("The internal id of an API resource within game data.");
            descriptor.UseNamedApiResourceField<VersionGameIndex, Version, VersionType>(x => x.Version);
        }
    }
}
