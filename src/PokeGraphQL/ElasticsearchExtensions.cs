// <copyright file="ElasticsearchExtensions.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Nest;

    internal static class ElasticsearchExtensions
    {
        internal static IServiceCollection AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var defaultIndex = configuration["elasticsearch:index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex);
                //.DefaultMappingFor<Post>(m => m
                //    .Ignore(p => p.IsPublished)
                //    .PropertyName(p => p.ID, "id")
                //)
                //.DefaultMappingFor<Comment>(m => m
                //    .Ignore(c => c.Email)
                //    .Ignore(c => c.IsAdmin)
                //    .PropertyName(c => c.ID, "id")
                //);

            services.AddSingleton<IElasticClient>(s => new ElasticClient(settings));
            return services;
        }
    }
}
