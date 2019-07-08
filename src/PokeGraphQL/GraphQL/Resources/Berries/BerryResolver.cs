namespace PokeGraphQL.GraphQL.Resources.Berries
{
    using System.Threading;
    using System.Threading.Tasks;
    using PokeAPI;

    internal class BerryResolver
    {
        public virtual async Task<Berry> GetBerryAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<Berry>(nameOrId).ConfigureAwait(false);

        public virtual async Task<BerryFirmness> GetBerryFirmnessAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<BerryFirmness>(nameOrId).ConfigureAwait(false);

        public virtual async Task<BerryFlavor> GetBerryFlavorAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<BerryFlavor>(nameOrId).ConfigureAwait(false);
    }
}
