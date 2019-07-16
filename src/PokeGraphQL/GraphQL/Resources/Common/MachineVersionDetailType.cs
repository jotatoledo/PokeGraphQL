// <copyright file="MachineVersionDetailType.cs" company="PokeGraphQL.Net">
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
    using PokeGraphQL.GraphQL.Resources.Machines;

    internal sealed class MachineVersionDetailType : ObjectType<MachineVersionDetail>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<MachineVersionDetail> descriptor)
        {
            descriptor.UseApiResourceField<MachineVersionDetail, Machine, MachineType>(x => x.Machine);
            descriptor.UseNamedApiResourceField<MachineVersionDetail, VersionGroup, VersionGroupType>(x => x.VersionGroup);
        }
    }
}
