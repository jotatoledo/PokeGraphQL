// <copyright file="MoveAilmentType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Moves
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal sealed class MoveAilmentType : BaseNamedApiObjectType<MoveAilment>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<MoveAilment> descriptor)
        {
            descriptor.Description("Move Ailments are status conditions caused by moves used during battle.");
            descriptor.UseNamedApiResourceCollectionField<MoveAilment, Move, MoveType>(x => x.Moves);
        }
    }
}
