// <copyright file="MoveResolver.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Moves
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using PokeAPI;

    public class MoveResolver
    {
        public virtual async Task<Move> GetMoveAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<Move>(nameOrId).ConfigureAwait(false);

        public virtual async Task<MoveAilment> GetMoveAilmentAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<MoveAilment>(nameOrId).ConfigureAwait(false);

        public virtual async Task<MoveDamageClass> GetMoveDamageClassAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<MoveDamageClass>(nameOrId).ConfigureAwait(false);

        public virtual async Task<MoveLearnMethod> GetMoveLearnMethodAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<MoveLearnMethod>(nameOrId).ConfigureAwait(false);

        public virtual async Task<MoveTarget> GetMoveTargetAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<MoveTarget>(nameOrId).ConfigureAwait(false);

        public virtual async Task<MoveCategory> GetMoveCategoryAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<MoveCategory>(nameOrId).ConfigureAwait(false);
    }
}
