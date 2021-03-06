﻿// <copyright file="ContestEffectType.cs" company="PokeGraphQL.Net">
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

    internal sealed class ContestEffectType : BaseApiObjectType<ContestEffect>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<ContestEffect> descriptor)
        {
            descriptor.Description("Contest effects refer to the effects of moves when used in contests.");
            descriptor.Field(x => x.Appeal)
                .Description("The base number of hearts the user of this move gets.");
            descriptor.Field(x => x.Jam)
                .Description("The base number of hearts the user's opponent loses.");
            descriptor.Field(x => x.EffectEntries)
                .Description("The result of this contest effect listed in different languages.")
                .Type<ListType<EffectsType>>();
            descriptor.Field(x => x.FlavorTextEntries)
                .Description("The flavor text of this contest effect listed in different languages.")
                .Type<ListType<FlavorTextType>>();
        }
    }
}
