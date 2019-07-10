namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class PokemonColorType : BaseNamedApiObjectType<PokemonColour>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<PokemonColour> descriptor)
        {
            descriptor.Description(@"Colors used for sorting pokémon in a pokédex. 
                The color listed in the Pokédex is usually the color most apparent or covering each Pokémon's body. 
                No orange category exists; Pokémon that are primarily orange are listed as red or brown.");
            descriptor.Field(x => x.Species)
                .Description("A list of the pokémon species that have this color.")
                .Type<ListType<PokemonSpeciesType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<PokemonResolver>();
                    var resourceTasks = ctx.Parent<PokemonColour>()
                        .Species
                        .Select(species => resolver.GetPokemonSpeciesAsync(species.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
        }
    }
}
