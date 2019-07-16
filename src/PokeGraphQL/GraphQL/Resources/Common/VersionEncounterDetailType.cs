// <copyright file="VersionEncounterDetailType.cs" company="PokeGraphQL.Net">
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

    internal sealed class VersionEncounterDetailType : ObjectType<VersionEncounterDetail>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<VersionEncounterDetail> descriptor)
        {
            descriptor.Field(x => x.EncounterDetails)
                .Type<ListType<EncounterType>>();
            descriptor.UseNamedApiResourceField<VersionEncounterDetail, Version, VersionType>(x => x.Version);
        }
    }
}
