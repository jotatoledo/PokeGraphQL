// <copyright file="UriExtensions.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL
{
    using System;
    using System.Linq;

    /// <summary>
    /// Provides commodity extenion methods to <see cref="Uri"/> and <see cref="string"/>
    /// </summary>
    internal static class UriExtensions
    {

        /// <summary>
        /// Extracts the last segment in an absolute path.
        /// </summary>
        /// <param name="uri">The source uri.</param>
        /// <returns>The last segment of the uri.</returns>
        internal static string LastSegment(this Uri uri) => uri.AbsoluteUri.LastSegment();

        /// <summary>
        /// Extracts the last segment in a <see cref="Uri"/> absolute path.
        /// </summary>
        /// <param name="uri">The source uri.</param>
        /// <returns>The last segment of the uri.</returns>
        internal static string LastSegment(this string uri) => uri.Split("/").Where(segm => !string.IsNullOrEmpty(segm)).Last();
    }
}
