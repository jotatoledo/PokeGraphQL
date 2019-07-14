// <copyright file="MachineVersionDetailType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources
{
    using System;
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Games;
    using PokeGraphQL.GraphQL.Resources.Machines;

    internal sealed class MachineVersionDetailType : ObjectType<MachineVersionDetail>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<MachineVersionDetail> descriptor)
        {
            descriptor.Field(x => x.Machine)
                .Description("The machine that teaches a move from an item.")
                .Type<MachineType>()
                .Resolver((ctx, token) => ctx.Service<MachineResolver>().GetMachineAsync(Convert.ToInt32(ctx.Parent<MachineVersionDetail>().Machine.Url.LastSegment()), token));
            descriptor.Field(x => x.VersionGroup)
                .Description("The version group of this specific machine.")
                .Type<VersionGroupType>()
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetVersionGroupAsync(ctx.Parent<MachineVersionDetail>().VersionGroup.Name, token));
        }
    }
}
