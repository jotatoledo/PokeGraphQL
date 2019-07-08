namespace PokeGraphQL
{
    using HotChocolate.AspNetCore;
    using HotChocolate.AspNetCore.GraphiQL;
    using HotChocolate.AspNetCore.Playground;
    using HotChocolate.AspNetCore.Voyager;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using PokeGraphQL.GraphQL;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddElasticsearch(this.Configuration);
            services.AddHotChocolate();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseGraphQL("/graphql")
                .UseGraphiQL("/graphql", "/graphiql")
                .UsePlayground("/graphql", "/playground" )
                .UseVoyager("/graphql", "/voyager" );
            app.UseMvc();
        }
    }
}
