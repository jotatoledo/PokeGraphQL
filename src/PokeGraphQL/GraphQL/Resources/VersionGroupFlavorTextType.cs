// <copyright file="VersionGroupFlavorTextType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Games;
    using PokeGraphQL.GraphQL.Resources.Languages;

    internal sealed class VersionGroupFlavorTextType : ObjectType<VersionGroupFlavorText>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<VersionGroupFlavorText> descriptor)
        {
            descriptor.Field(x => x.Text)
                .Description("The localized name for an api resource in a specific language.");
            descriptor.Field(x => x.Language)
                .Description("The language this name is in.")
                .Type<LanguageType>()
                .Resolver((ctx, token) => ctx.Service<LanguageResolver>().GetLanguageAsync(ctx.Parent<VersionGroupFlavorText>().Language.Name, token));
            descriptor.Field(x => x.VersionGroup)
                .Type<VersionGroupType>()
                .Description("The version group which uses this flavor text.")
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetVersionGroupAsync(ctx.Parent<VersionGroupFlavorText>().VersionGroup.Name, token));
        }
    }
}
