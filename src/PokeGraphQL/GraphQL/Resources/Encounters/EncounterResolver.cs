namespace PokeGraphQL.GraphQL.Resources.Encounters
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using PokeAPI;

    public class EncounterResolver
    {
        public virtual async Task<EncounterMethod> GetEncounterMethodAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<EncounterMethod>(nameOrId).ConfigureAwait(false);

        public virtual async Task<EncounterCondition> GetEncounterConditionAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<EncounterCondition>(nameOrId).ConfigureAwait(false);

        public virtual async Task<EncounterConditionValue> GetEncounterConditionValueAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<EncounterConditionValue>(nameOrId).ConfigureAwait(false);
    }
}
