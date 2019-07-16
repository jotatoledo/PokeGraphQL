// <copyright file="AbilityEffectChangeType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Common
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Games;

    internal sealed class AbilityEffectChangeType : ObjectType<AbilityEffectChange>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<AbilityEffectChange> descriptor)
        {
            // TODO type should be changed upstream to `List<Effects>`
            // See 'effect_changes' structure in https://pokeapi.co/api/v2/ability/1
            descriptor.Field(x => x.EffectEntries)
                .Description("The previous effect of this ability listed in different languages.")
                .Type<ListType<EffectsType>>();
            descriptor.UseNamedApiResourceField<AbilityEffectChange, VersionGroup, VersionGroupType>(x => x.VersionGroup);
        }
    }
}
