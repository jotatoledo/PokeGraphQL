// <copyright file="VersionGroupFlavorTextType.cs" company="PokeGraphQL.Net">
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
    using PokeGraphQL.GraphQL.Resources.Languages;

    internal sealed class VersionGroupFlavorTextType : ObjectType<VersionGroupFlavorText>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<VersionGroupFlavorText> descriptor)
        {
            descriptor.Field(x => x.Text)
                .Description("The localized name for an api resource in a specific language.");
            descriptor.UseNamedApiResourceField<VersionGroupFlavorText, Language, LanguageType>(x => x.Language);
            descriptor.UseNamedApiResourceField<VersionGroupFlavorText, VersionGroup, VersionGroupType>(x => x.VersionGroup);
        }
    }
}
