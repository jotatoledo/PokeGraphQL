// <copyright file="EffectType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources
{
    using HotChocolate.Types;
    using PokeAPI;
    using PokeGraphQL.GraphQL.Resources.Languages;

    internal sealed class EffectType : ObjectType<Effect>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<Effect> descriptor)
        {
            descriptor.FixStructType();
            descriptor.Field(x => x.Text)
                .Description("The localized effect text for an api resource in a specific language.");
            descriptor.Field(x => x.Language)
                .Description("The language this effect is in.")
                .Type<LanguageType>()
                .Resolver((ctx, token) => ctx.Service<LanguageResolver>().GetLanguageAsync(ctx.Parent<Effect>().Language.Name, token));
        }
    }
}
