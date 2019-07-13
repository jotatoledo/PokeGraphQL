// <copyright file="ConfigureForwardedHeaders.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    public sealed class ConfigureForwardedHeaders : IConfigureOptions<ForwardedHeadersOptions>
    {
        private readonly IConfiguration configuration;

        public ConfigureForwardedHeaders(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <inheritdoc/>
        public void Configure(ForwardedHeadersOptions options)
        {
            options.ForwardLimit = 2;
            options.ForwardedHeaders = ForwardedHeaders.All;

            // Only loopback proxies are allowed by default.
            // Clear that restriction because forwarders are enabled by explicit configuration.
            options.KnownProxies.Clear();
            options.KnownNetworks.Clear();

            var webHost = this.configuration.GetValue<string>("ASPNETCORE_WEBHOST");
            if (!string.IsNullOrEmpty(webHost))
            {
                options.AllowedHosts.Add(webHost);
            }
        }
    }
}
