// <copyright file="MoveDamageClassType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Moves
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal sealed class MoveDamageClassType : BaseNamedApiObjectType<MoveDamageClass>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<MoveDamageClass> descriptor)
        {
            descriptor.Description("Damage classes moves can have, e.g. physical, special, or status (non-damaging).");
            descriptor.Ignore(x => x.Descriptions);
            descriptor.UseNamedApiResourceCollectionField<MoveDamageClass, Move, MoveType>(x => x.Moves);
        }
    }
}
