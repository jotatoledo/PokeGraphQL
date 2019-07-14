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
    using System.Threading.Tasks;

    public class ListResolver
    {
        public virtual Task<IReadOnlyCollection<TResource>> GetItemsAsync<TResource>(int? limit, int? offset)
        {
            throw new NotImplementedException();
        }
    }
}
