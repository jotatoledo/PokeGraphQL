namespace PokeGraphQL.GraphQL
{
    using System;
    using System.Linq;

    public static class UriExtensions
    {
        public static string LastSegment(this Uri uri) => uri.AbsoluteUri.Split("/").Where(segm => !string.IsNullOrEmpty(segm)).Last();
    }
}
