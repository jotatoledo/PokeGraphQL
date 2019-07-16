// <copyright file="MoveCategoryType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Moves
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal sealed class MoveCategoryType : BaseNamedApiObjectType<MoveCategory>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<MoveCategory> descriptor)
        {
            descriptor.Description("Very general categories that loosely group move effects.");
            descriptor.Ignore(x => x.Descriptions);
            descriptor.UseNamedApiResourceCollectionField<MoveCategory, Move, MoveType>(x => x.Moves);
        }
    }
}
