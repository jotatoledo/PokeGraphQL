// <copyright file="ListResolver.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using DotNetFunctional.Maybe;
    using HotChocolate.Types.Relay;
    using HotChocolate.Utilities;
    using Newtonsoft.Json;
    using PokeApiNet.Data;
    using PokeApiNet.Models;

    internal class ListResolver
    {
        private readonly PokeApiClient pokeApiClient;

        public ListResolver(PokeApiClient pokeApiClient)
        {
            this.pokeApiClient = pokeApiClient;
        }

        internal virtual async Task<Connection<TResource>> GetNamedItemsAsync<TResource>(IDictionary<string, object> cursorProps, int? pageSize = 20, CancellationToken cancellationToken = default)
            where TResource : NamedApiResource
        {
            var cursor = ParseCursor(cursorProps);
            var resourcePage = await this.pokeApiClient.GetNamedResourcePageAsync<TResource>(pageSize.Value, cursor.OffSet, cancellationToken);
            var unrappedValues = await Task.WhenAll(resourcePage.Results.Select(val => this.pokeApiClient.GetResourceAsync(val, cancellationToken)));
            var edgeFactory = CreateEdgeFactory<TResource>(pageSize.Value);
            var edges = unrappedValues.Select(edgeFactory).ToList().AsReadOnly();
            return new Connection<TResource>(new ResourcePageInfo<TResource>(pageSize.Value, cursor.OffSet, resourcePage), edges);
        }

        internal virtual async Task<Connection<TResource>> GetApiItemsAsync<TResource>(IDictionary<string, object> cursorProps, int? pageSize = 20, CancellationToken cancellationToken = default)
            where TResource : ApiResource
        {
            var cursor = ParseCursor(cursorProps);
            var resourcePage = await this.pokeApiClient.GetApiResourcePageAsync<TResource>(pageSize.Value, cursor.OffSet, cancellationToken);
            var unrappedValues = await Task.WhenAll(resourcePage.Results.Select(val => this.pokeApiClient.GetResourceAsync(val, cancellationToken)));
            var edgeFactory = CreateEdgeFactory<TResource>(pageSize.Value);
            var edges = unrappedValues.Select(edgeFactory).ToList().AsReadOnly();
            return new Connection<TResource>(new ResourcePageInfo<TResource>(pageSize.Value, cursor.OffSet, resourcePage), edges);
        }

        private static Cursor ParseCursor(IDictionary<string, object> cursorProps) => JsonConvert.DeserializeObject<Cursor>(JsonConvert.SerializeObject(cursorProps));

        private static string SerializeCursor(int offSet) => Base64Serializer.Serialize(new Cursor { OffSet = offSet });

        private static Func<TResource, int, Edge<TResource>> CreateEdgeFactory<TResource>(int pageSize) =>
            (resource, index) => new Edge<TResource>(SerializeCursor(pageSize + index), resource);

        private sealed class Cursor
        {
            public int OffSet { get; set; }
        }

        private sealed class ResourcePageInfo<TResource> : IPageInfo
            where TResource : ResourceBase
        {
            public ResourcePageInfo(int pageSize, int offset, ResourceList<TResource> page)
            {
                this.HasNextPage = !string.IsNullOrEmpty(page.Next);
                this.HasPreviousPage = !string.IsNullOrEmpty(page.Previous);
                this.TotalCount = page.Count;
                this.StartCursor = SerializeCursor(offset);
                this.EndCursor = SerializeCursor(offset + pageSize);
            }

            public bool HasNextPage { get; internal set; }

            public bool HasPreviousPage { get; internal set; }

            public string StartCursor { get; internal set; }

            public string EndCursor { get; internal set; }

            public long? TotalCount { get; internal set; }
        }
    }
}
