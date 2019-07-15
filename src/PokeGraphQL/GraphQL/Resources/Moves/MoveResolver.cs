// <copyright file="MoveResolver.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Moves
{
    using System.Threading;
    using System.Threading.Tasks;
    using PokeApiNet.Data;
    using PokeApiNet.Models;

    public class MoveResolver
    {
        private readonly PokeApiClient pokeApiClient;

        public MoveResolver(PokeApiClient pokeApiClient)
        {
            this.pokeApiClient = pokeApiClient;
        }

        public virtual async Task<Move> GetMoveAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<Move>(nameOrId).ConfigureAwait(false);

        public virtual async Task<MoveAilment> GetMoveAilmentAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<MoveAilment>(nameOrId).ConfigureAwait(false);

        public virtual async Task<MoveDamageClass> GetMoveDamageClassAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<MoveDamageClass>(nameOrId).ConfigureAwait(false);

        public virtual async Task<MoveLearnMethod> GetMoveLearnMethodAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<MoveLearnMethod>(nameOrId).ConfigureAwait(false);

        public virtual async Task<MoveTarget> GetMoveTargetAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<MoveTarget>(nameOrId).ConfigureAwait(false);

        public virtual async Task<MoveCategory> GetMoveCategoryAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<MoveCategory>(nameOrId).ConfigureAwait(false);

        public virtual async Task<MoveBattleStyle> GetMoveBattleStyleAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<MoveBattleStyle>(nameOrId).ConfigureAwait(false);
    }
}
