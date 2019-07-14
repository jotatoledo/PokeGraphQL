// <copyright file="FlavorTextType.cs" company="PokeGraphQL.Net">
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

    internal sealed class FlavorTextType : ObjectType<FlavorText>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<FlavorText> descriptor)
        {
            descriptor.FixStructType();
            descriptor.Field(x => x.Text)
                .Description("The localized name for an api resource in a specific language.");
            descriptor.Field(x => x.Language)
                .Description("The language this flavor text is in")
                .Type<LanguageType>()
                .Resolver((ctx, token) => ctx.Service<LanguageResolver>().GetLanguageAsync(ctx.Parent<FlavorText>().Language.Name, token));
        }
    }
}
