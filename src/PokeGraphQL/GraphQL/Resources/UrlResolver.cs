// <copyright file="UrlResolver.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class UrlResolver
    {
        private readonly HttpClient httpClient;

        public UrlResolver(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public virtual async Task<T> GetAsync<T>(string path, CancellationToken cancellationToken = default)
        {
            // TODO handle cache
            var response = await this.httpClient.GetAsync(new Uri(path, UriKind.Absolute));
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}
