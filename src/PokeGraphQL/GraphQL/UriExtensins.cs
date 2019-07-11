// <copyright file="UriExtensins.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL
{
    using System;
    using System.Linq;

    public static class UriExtensions
    {
        public static string LastSegment(this Uri uri) => uri.AbsoluteUri.Split("/").Where(segm => !string.IsNullOrEmpty(segm)).Last();
    }
}
