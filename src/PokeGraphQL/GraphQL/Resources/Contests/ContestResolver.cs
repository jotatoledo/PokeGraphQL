namespace PokeGraphQL.GraphQL.Resources.Contests
{
    using System.Threading;
    using System.Threading.Tasks;
    using PokeAPI;

    public class ContestResolver
    {
        public virtual async Task<ContestType> GetContestTypeAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<ContestType>(nameOrId).ConfigureAwait(false);

        public virtual async Task<ContestEffect> GetContestEffectAsync(int id, CancellationToken cancellationToken = default) => await DataFetcher.GetApiObject<ContestEffect>(id).ConfigureAwait(false);

        public virtual async Task<SuperContestEffect> GetSuperContestEffectAsync(int id, CancellationToken cancellationToken = default) => await DataFetcher.GetApiObject<SuperContestEffect>(id).ConfigureAwait(false);
    }
}
