// <copyright file="AbilityEffectChangeType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources
{
    using HotChocolate.Types;
    using PokeAPI;
    using PokeGraphQL.GraphQL.Resources.Games;

    internal sealed class AbilityEffectChangeType : ObjectType<AbilityEffectChange>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<AbilityEffectChange> descriptor)
        {
            descriptor.FixStructType();

            // TODO type should be changed upstream to `Effect[]`.
            // See 'effect_changes' structure in https://pokeapi.co/api/v2/ability/1
            descriptor.Field(x => x.Effects)
                .Description("The previous effect of this ability listed in different languages.")
                .Type<ListType<EffectType>>();
            descriptor.Field(x => x.VersionGroup)
                .Description("The version group in which the previous effect of this ability originated.")
                .Type<VersionGroupType>()
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetVersionGroupAsync(ctx.Parent<AbilityEffectChange>().VersionGroup.Name, token));
        }
    }
}
