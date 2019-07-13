// <copyright file="ConfigureHsts.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL
{
    using System;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.Extensions.Options;

    public sealed class ConfigureHsts : IConfigureOptions<HstsOptions>
    {
        private static readonly int SecondsInYear = 31536000;

        /// <inheritdoc/>
        public void Configure(HstsOptions options)
        {
            options.Preload = true;
            options.IncludeSubDomains = true;
            options.MaxAge = TimeSpan.FromSeconds(SecondsInYear);
        }
    }
}
