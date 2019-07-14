// <copyright file="MachineType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Machines
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Games;
    using PokeGraphQL.GraphQL.Resources.Items;
    using PokeGraphQL.GraphQL.Resources.Moves;

    internal sealed class MachineType : BaseApiObjectType<Machine>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Machine> descriptor)
        {
            descriptor.Description(@"Machines are the representation of items that teach moves to Pokémon. 
                They vary from version to version, so it is not certain that one specific TM or HM corresponds to a single Machine.");
            descriptor.Field(x => x.Item)
                .Description("The TM or HM item that corresponds to this machine.")
                .Type<ItemType>()
                .Resolver((ctx, token) => ctx.Service<ItemResolver>().GetItemAsync(ctx.Parent<Machine>().Item.Name, token));
            descriptor.Field(x => x.Move)
                .Description("The move that is taught by this machine.")
                .Type<MoveType>()
                .Resolver((ctx, token) => ctx.Service<MoveResolver>().GetMoveAsync(ctx.Parent<Machine>().Move.Name, token));
            descriptor.Field(x => x.VersionGroup)
                .Description("The version group that this machine applies to.")
                .Type<VersionGroupType>()
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetVersionGroupAsync(ctx.Parent<Machine>().VersionGroup.Name, token));
        }
    }
}
