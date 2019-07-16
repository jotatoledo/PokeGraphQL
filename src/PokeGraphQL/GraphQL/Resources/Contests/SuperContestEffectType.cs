// <copyright file="SuperContestEffectType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Contests
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Common;
    using PokeGraphQL.GraphQL.Resources.Moves;

    internal sealed class SuperContestEffectType : BaseApiObjectType<SuperContestEffect>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<SuperContestEffect> descriptor)
        {
            descriptor.Description("Super contest effects refer to the effects of moves when used in super contests.");
            descriptor.Field(x => x.Appeal)
                .Description("The level of appeal this super contest effect has.");
            descriptor.UseNamedApiResourceCollectionField<SuperContestEffect, Move, MoveType>(x => x.Moves);
            descriptor.Field(x => x.FlavorTextEntries)
                .Description("The flavor text of this contest effect listed in different languages.")
                .Type<ListType<FlavorTextType>>();
        }
    }
}
