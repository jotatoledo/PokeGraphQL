// <copyright file="MoveBattleStyleType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Moves
{
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class MoveBattleStyleType : BaseNamedApiObjectType<MoveBattleStyle>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<MoveBattleStyle> descriptor)
        {
            descriptor.Description("Styles of moves when used in the Battle Palace.");
        }
    }
}
