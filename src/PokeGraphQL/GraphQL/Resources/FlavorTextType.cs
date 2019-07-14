// <copyright file="FlavorTextType.cs" company="PokeGraphQL.Net">
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

    internal sealed class FlavorTextType : ObjectType<FlavorTexts>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<FlavorTexts> descriptor)
        {
            descriptor.Field(x => x.FlavorText)
                .Description("The localized name for an api resource in a specific language.");
            descriptor.Field(x => x.Language)
                .Description("The language this flavor text is in")
                .Type<LanguageType>()
                .Resolver((ctx, token) => ctx.Service<LanguageResolver>().GetLanguageAsync(ctx.Parent<FlavorTexts>().Language.Name, token));
        }
    }
}
