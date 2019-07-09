namespace PokeGraphQL.GraphQL.Resources.Games
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;
    using PokeGraphQL.GraphQL.Resources.Locations;

    internal sealed class PokedexType : BaseNamedApiObjectType<Pokedex>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Pokedex> descriptor)
        {
            // TODO: implement ignored fields
            descriptor.Description(@"A Pokédex is a handheld electronic encyclopedia device; one which is capable of recording 
            and retaining information of the various Pokémon in a given region with the exception of the national dex
            and some smaller dexes related to portions of a region.");
            descriptor.Field(x => x.IsMainSeries)
                .Description("Whether or not this pokédex originated in the main series of the video games.");
            descriptor.Field(x => x.Descriptions)
                .Description("The description of this pokédex listed in different languages.")
                .Ignore();
            descriptor.Field(x => x.Entries)
                .Name("pokemonEntries")
                .Description("A list of pokémon catalogued in this pokédex  and their indexes.")
                .Ignore();
            descriptor.Field(x => x.Region)
                .Description("The region this pokédex catalogues pokémon for.")
                .Type<RegionType>()
                .Resolver((ctx, token) => ctx.Service<LocationResolver>().GetRegionAsync(ctx.Parent<Pokedex>().Region.Name, token));
            descriptor.Field(x => x.VersionGroups)
                .Description("A list of version groups this pokédex is relevent to.")
                .Type<ListType<VersionGroupType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<GameResolver>();
                    var resourceTasks = ctx.Parent<Pokedex>()
                    .VersionGroups
                    .Select(versionGroup => resolver.GetVersionGroupAsync(versionGroup.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
        }
    }
}
