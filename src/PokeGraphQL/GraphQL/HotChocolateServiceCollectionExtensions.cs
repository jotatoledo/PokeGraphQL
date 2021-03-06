﻿// <copyright file="HotChocolateServiceCollectionExtensions.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL
{
    using HotChocolate;
    using HotChocolate.Execution.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using PokeGraphQL.GraphQL.Resources;
    using PokeGraphQL.GraphQL.Resources.Items;
    using PokeGraphQL.GraphQL.Resources.Moves;
    using PokeGraphQL.GraphQL.Resources.Pokemons;

    internal static class HotChocolateServiceCollectionExtensions
    {
        internal static IServiceCollection AddHotChocolate(this IServiceCollection services)
        {
            services.AddScoped<ItemResolver>();
            services.AddScoped<PokemonResolver>();
            services.AddScoped<MoveResolver>();
            services.AddScoped<UrlResolver>();
            services.AddScoped<ListResolver>();

            return services.AddGraphQL(
                sp => SchemaBuilder.New()
              .AddServices(sp)
              .AddQueryType<PokeApiQuery>()
              .Create(),
                new QueryExecutionOptions
                {
                    TracingPreference = TracingPreference.OnDemand,
                });
        }
    }
}
