// <copyright file="MoveLearnMethodType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Moves
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Games;

    internal sealed class MoveLearnMethodType : BaseNamedApiObjectType<MoveLearnMethod>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<MoveLearnMethod> descriptor)
        {
            descriptor.Description("Methods by which pokémon can learn moves.");
            descriptor.Ignore(x => x.Descriptions);
            descriptor.UseNamedApiResourceCollectionField<MoveLearnMethod, VersionGroup, VersionGroupType>(x => x.VersionGroups);
        }
    }
}
