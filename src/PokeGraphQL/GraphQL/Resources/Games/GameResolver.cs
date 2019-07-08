namespace PokeGraphQL.GraphQL.Resources.Games
{
    using System.Threading;
    using System.Threading.Tasks;
    using PokeAPI;

    public class GameResolver
    {
        public virtual async Task<Generation> GetGenerationAsync(string nameOrId, CancellationToken token = default) => await DataFetcher.GetNamedApiObject<Generation>(nameOrId).ConfigureAwait(false);

        public virtual async Task<GameVersion> GetVersionAsync(string nameOrId, CancellationToken token = default) => await DataFetcher.GetNamedApiObject<GameVersion>(nameOrId).ConfigureAwait(false);

        public virtual async Task<VersionGroup> GetVersionGroupAsync(string nameOrId, CancellationToken token = default) => await DataFetcher.GetNamedApiObject<VersionGroup>(nameOrId).ConfigureAwait(false);

        public virtual async Task<Pokedex> GetPokedexAsync(string nameOrId, CancellationToken token = default) => await DataFetcher.GetNamedApiObject<Pokedex>(nameOrId).ConfigureAwait(false);
    }
}
