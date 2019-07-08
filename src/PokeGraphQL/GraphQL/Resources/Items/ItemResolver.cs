namespace PokeGraphQL.GraphQL.Resources.Items
{
    using System.Threading;
    using System.Threading.Tasks;
    using PokeAPI;

    public class ItemResolver
    {
        public virtual async Task<Item> GetItemAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<Item>(nameOrId).ConfigureAwait(false);

        public virtual async Task<ItemAttribute> GetAttributeAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<ItemAttribute>(nameOrId).ConfigureAwait(false);

        public virtual async Task<ItemCategory> GetCategoryAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<ItemCategory>(nameOrId).ConfigureAwait(false);

        public virtual async Task<ItemPocket> GetPocketAsync(string nameOrId, CancellationToken token = default) => await DataFetcher.GetNamedApiObject<ItemPocket>(nameOrId).ConfigureAwait(false);

        public virtual async Task<ItemFlingEffect> GetFlingEffectAsync(string nameOrId, CancellationToken token = default) => await DataFetcher.GetNamedApiObject<ItemFlingEffect>(nameOrId).ConfigureAwait(false);
    }
}