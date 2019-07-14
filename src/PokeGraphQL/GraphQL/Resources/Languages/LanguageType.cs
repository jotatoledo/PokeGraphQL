// <copyright file="LanguageType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Languages
{
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class LanguageType : BaseNamedApiObjectType<Language>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Language> descriptor)
        {
            descriptor.Description("Languages for translations of api resource information.");
            descriptor.Field(x => x.IsOfficial)
                .Description("Whether or not the games are published in this language.");
            descriptor.Field(x => x.Iso639)
                .Description("The two-letter code of the country where this language is spoken. Note that it is not unique.");
            descriptor.Field(x => x.Iso3166)
                .Description("The two-letter code of the language. Note that it is not unique.");
        }
    }
}
