namespace PokeGraphQL.GraphQL.Resources.Games
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class GenerationType : BaseNamedApiObjectType<Generation>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Generation> descriptor)
        {
            // TODO add missing props
            descriptor.Description(@"A generation is a grouping of the Pokémon games that separates them based on the Pokémon they include.
            In each generation, a new set of Pokémon, Moves, Abilities and Types that did not exist in the previous generation are released.");
            descriptor.Field(x => x.Abilities)
                .Description("A list of abilities that were introduced in this generation.")
                .Ignore();
            descriptor.Field(x => x.MainRegion)
                .Description("The main region travelled in this generation.")
                .Ignore();
            descriptor.Field(x => x.Moves)
                .Description("A list of moves that were introduced in this generation.")
                .Ignore();
            descriptor.Field(x => x.Species)
                .Name("pokemonSpecies")
                .Description("A list of pokémon species that were introduced in this generation.")
                .Ignore();
            descriptor.Field(x => x.Types)
                .Description("A list of types that were introduced in this generation.")
                .Ignore();
            descriptor.Field(x => x.VersionGroups)
                .Description("A list of version groups that were introduced in this generation.")
                .Type<ListType<VersionGroupType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<GameResolver>();
                    var resourceTasks = ctx.Parent<Generation>()
                    .VersionGroups
                    .Select(versionGroup => resolver.GetVersionAsync(versionGroup.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
        }
    }
}
