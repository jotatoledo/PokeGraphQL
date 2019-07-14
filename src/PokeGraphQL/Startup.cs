// <copyright file="Startup.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL
{
    using System;
    using System.Net.Http;
    using HotChocolate.AspNetCore;
    using HotChocolate.AspNetCore.GraphiQL;
    using HotChocolate.AspNetCore.Voyager;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using PokeApiNet.Data;
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
            services.AddSingleton<PokeApiClient>();
            services.AddScoped(s =>
            {
                // TODO clean up
                var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("PokegraphQL (https://github.com/jotatoledo/PokeGraphQL)");
                return client;
            });

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

            app.UseXfo(options => options.Deny())
                .UseXXssProtection(options => options.EnabledWithBlockMode())
                .UseXContentTypeOptions()
                .UseReferrerPolicy(options => options.StrictOriginWhenCrossOrigin())
                .UseCsp(options => options
                    .DefaultSources(s => s.None())
                    .ScriptSources(s => s.Self().UnsafeInline())
                    .StyleSources(s => s.Self().UnsafeInline())
                    .ObjectSources(s => s.None())
                    .ImageSources(s => s.Self())
                    .MediaSources(s => s.None())
                    .FrameSources(s => s.None())
                    .FontSources(s => s.None())
                    .ConnectSources(s => s.Self())
                    .BaseUris(s => s.None())
                    .FrameAncestors(s => s.None())
                    .FormActions(s => s.None())
                    .WorkerSources(s => s.Self().CustomSources("blob:"))
                    .ManifestSources(s => s.None()));

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
