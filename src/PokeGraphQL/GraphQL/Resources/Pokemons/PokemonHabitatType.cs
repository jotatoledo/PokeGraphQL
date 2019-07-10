namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class PokemonHabitatType : BaseNamedApiObjectType<PokemonHabitat>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<PokemonHabitat> descriptor)
        {
            descriptor.Description(@"Habitats are generally different terrain pokémon can be found in 
                but can also be areas designated for rare or legendary pokémon.");
            descriptor.Field(x => x.Species)
                .Description("A list of the pokémon species that can be found in this habitat.")
                .Type<ListType<PokemonSpeciesType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<PokemonResolver>();
                    var resourceTasks = ctx.Parent<PokemonHabitat>()
                        .Species
                        .Select(species => resolver.GetPokemonSpeciesAsync(species.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
        }
    }
}
