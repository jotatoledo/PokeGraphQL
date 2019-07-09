namespace PokeGraphQL.GraphQL.Resources.Locations
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;
    using PokeGraphQL.GraphQL.Resources.Games;

    internal sealed class RegionType : BaseNamedApiObjectType<Region>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Region> descriptor)
        {
            descriptor.Description(@"A region is an organized area of the pokémon world. 
            Most often, the main difference between regions is the species of pokémon that can be encountered within them.");
            descriptor.Field(x => x.Locations)
                .Description("A list of locations that can be found in this region.")
                .Type<ListType<LocationType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<LocationResolver>();
                    var resourceTasks = ctx.Parent<Region>()
                        .Locations
                        .Select(location => resolver.GetLocationAsync(location.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
            descriptor.Field(x => x.MainGeneration)
                .Description("The generation this region was introduced in.")
                .Type<GenerationType>()
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetGenerationAsync(ctx.Parent<Region>().MainGeneration.Name, token));
            descriptor.Field(x => x.Pokedices)
                .Name("pokedexes")
                .Description("A list of pokédexes that catalogue pokemon in this region.")
                .Type<ListType<PokedexType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<GameResolver>();
                    var resourceTasks = ctx.Parent<Region>()
                        .Pokedices
                        .Select(pokedex => resolver.GetPokedexAsync(pokedex.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
            descriptor.Field(x => x.VersionGroups)
                .Description("A list of version groups where this region can be visited.")
                .Type<ListType<VersionGroupType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<GameResolver>();
                    var resourceTasks = ctx.Parent<Region>()
                        .VersionGroups
                        .Select(versionGroup => resolver.GetVersionGroupAsync(versionGroup.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
        }
    }
}
