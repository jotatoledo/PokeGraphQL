namespace PokeGraphQL.GraphQL.Resources.Locations
{
    using System.Threading;
    using System.Threading.Tasks;
    using PokeAPI;

    public class LocationResolver
    {
        public virtual async Task<Location> GetLocationAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<Location>(nameOrId).ConfigureAwait(false);

        public virtual async Task<LocationArea> GetLocationAreaAsync(int id, CancellationToken cancellationToken = default) => await DataFetcher.GetApiObject<LocationArea>(id).ConfigureAwait(false);

        public virtual async Task<PalParkArea> GetPalParkAreaAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<PalParkArea>(nameOrId).ConfigureAwait(false);

        public virtual async Task<Region> GetRegionAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<Region>(nameOrId).ConfigureAwait(false);
    }
}
