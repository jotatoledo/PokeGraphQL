// <copyright file="MoveTargetType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Moves
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal sealed class MoveTargetType : BaseNamedApiObjectType<MoveTarget>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<MoveTarget> descriptor)
        {
            descriptor.Description("Targets moves can be directed at during battle. Targets can be pokémon, environments or even other moves.");
            descriptor.Ignore(x => x.Descriptions);
            descriptor.UseNamedApiResourceCollectionField<MoveTarget, Move, MoveType>(x => x.Moves);
        }
    }
}
