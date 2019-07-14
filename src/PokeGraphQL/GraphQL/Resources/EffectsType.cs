// <copyright file="EffectType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Languages;

    internal sealed class EffectsType : ObjectType<Effects>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<Effects> descriptor)
        {
            descriptor.Field(x => x.Effect)
                .Description("The localized effect text for an api resource in a specific language.");
            descriptor.Field(x => x.Language)
                .Description("The language this effect is in.")
                .Type<LanguageType>()
                .Resolver((ctx, token) => ctx.Service<LanguageResolver>().GetLanguageAsync(ctx.Parent<Effects>().Language.Name, token));
        }
    }
}
