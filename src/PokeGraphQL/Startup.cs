// <copyright file="Startup.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL
{
    using System;
    using HotChocolate.AspNetCore;
    using HotChocolate.AspNetCore.GraphiQL;
    using HotChocolate.AspNetCore.Voyager;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using PokeGraphQL.GraphQL;

    public class Startup
    {
        private readonly ILogger logger;

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger("Heroku");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHotChocolate();

            if (string.Equals(this.Configuration.GetValue<string>("ASPNETCORE_HSTS_ENABLED"), "true", StringComparison.OrdinalIgnoreCase))
            {
                services.ConfigureOptions<ConfigureHsts>();
            }

            if (string.Equals(this.Configuration.GetValue<string>("ASPNETCORE_FORWARDEDHEADERS_ENABLED"), "true", StringComparison.OrdinalIgnoreCase))
            {
                services.ConfigureOptions<ConfigureForwardedHeaders>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (string.Equals(this.Configuration.GetValue<string>("ASPNETCORE_FORWARDEDHEADERS_ENABLED"), "true", StringComparison.OrdinalIgnoreCase))
            {
                app.UseForwardedHeaders();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
                if (string.Equals(this.Configuration.GetValue<string>("ASPNETCORE_HSTS_ENABLED"), "true", StringComparison.OrdinalIgnoreCase))
                {
                    app.UseHsts();
                }
            }

            app.UseGraphQL("/graphql")
                .UseGraphiQL(new GraphiQLOptions
                {
                    EnableSubscription = false,
                    QueryPath = "/graphql",
                    Path = "/graphiql",
                })
                .UsePlayground("/graphql", "/playground")
                .UseVoyager("/graphql", "/voyager");
            app.UseMvc();
        }
    }
}
