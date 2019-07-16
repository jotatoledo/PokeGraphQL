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
            descriptor.UseNamedApiResourceField<Machine, Item, ItemType>(x => x.Item);
            descriptor.UseNamedApiResourceField<Machine, Move, MoveType>(x => x.Move);
            descriptor.UseNamedApiResourceField<Machine, VersionGroup, VersionGroupType>(x => x.VersionGroup);
        }
    }
}
