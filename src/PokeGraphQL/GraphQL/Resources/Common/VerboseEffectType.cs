// <copyright file="VerboseEffectType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Common
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Languages;

    internal sealed class VerboseEffectType : ObjectType<VerboseEffect>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<VerboseEffect> descriptor)
        {
            descriptor.Field(x => x.Effect)
                .Description("The localized effect text for an api resource in a specific language.");
            descriptor.Field(x => x.ShortEffect)
                .Description("The localized effect text in brief.");
            descriptor.UseNamedApiResourceField<VerboseEffect, Language, LanguageType>(x => x.Language);
        }
    }
}
