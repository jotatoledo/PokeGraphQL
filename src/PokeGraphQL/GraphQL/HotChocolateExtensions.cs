namespace PokeGraphQL.GraphQL
{
    using HotChocolate;
    using HotChocolate.Execution.Configuration;
    using HotChocolate.Utilities;
    using Microsoft.Extensions.DependencyInjection;
    using PokeAPI;
    using PokeGraphQL.GraphQL.Converters;
    using PokeGraphQL.GraphQL.Resources.Berries;
    using PokeGraphQL.GraphQL.Resources.Contests;
    using PokeGraphQL.GraphQL.Resources.Encounters;
    using PokeGraphQL.GraphQL.Resources.Games;
    using PokeGraphQL.GraphQL.Resources.Items;

    internal static class HotChocolateExtensions
    {
        internal static IServiceCollection AddHotChocolate(this IServiceCollection services)
        {
            services.AddScoped<BerryResolver>();
            services.AddScoped<ItemResolver>();
            services.AddScoped<ContestResolver>();
            services.AddScoped<EncounterResolver>();
            services.AddScoped<GameResolver>();

            services.AddGraphQL(
                sp => SchemaBuilder.New()
              .AddServices(sp)
              .AddQueryType<PokeApiQuery>()
              .Create(),
                new QueryExecutionOptions
                {
                    TracingPreference = TracingPreference.OnDemand,
                });

            services.AddConversion();

            return services;
        }

        private static IServiceCollection AddConversion(this IServiceCollection services)
        {
            services.AddSingleton<ITypeConversion, TypeConversion>();
            services.AddSingleton<ITypeConverter, TimeSpanConverter>();
            return services;
        }
    }
}
