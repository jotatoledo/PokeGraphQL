// <copyright file="VersionType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Games
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal sealed class VersionType : BaseNamedApiObjectType<Version>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Version> descriptor)
        {
            descriptor.Description("Versions of the games, e.g., Red, Blue or Yellow.");
            descriptor.UseNamedApiResourceField<Version, VersionGroup, VersionGroupType>(x => x.VersionGroup);
        }
    }
}
