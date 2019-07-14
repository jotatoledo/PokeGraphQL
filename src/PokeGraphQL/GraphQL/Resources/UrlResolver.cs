namespace PokeGraphQL.GraphQL.Resources
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using PokeAPI;

    public class UrlResolver
    {
        public virtual Task<T> GetAsync<T>(string uri, CancellationToken cancellationToken = default) => this.GetAsync<T>(new Uri(uri, UriKind.Absolute));

        public virtual async Task<T> GetAsync<T>(Uri uri, CancellationToken cancellationToken = default) => await DataFetcher.GetAny<T>(uri).ConfigureAwait(false);
    }
}
