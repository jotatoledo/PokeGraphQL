// <copyright file="PokemonResolver.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using PokeApiNet.Data;
    using PokeApiNet.Models;
    using Type = PokeApiNet.Models.Type;

    public class PokemonResolver
    {
        private readonly PokeApiClient pokeApiClient;

        public PokemonResolver(PokeApiClient pokeApiClient)
        {
            this.pokeApiClient = pokeApiClient;
        }

        public virtual async Task<Ability> GetAbilityAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<Ability>(nameOrId).ConfigureAwait(false);

        public virtual async Task<Characteristic> GetCharacteristicAsync(int id, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceAsync<Characteristic>(id).ConfigureAwait(false);

        public virtual async Task<EggGroup> GetEggGroupAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<EggGroup>(nameOrId).ConfigureAwait(false);

        public virtual async Task<Gender> GetGenderAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<Gender>(nameOrId).ConfigureAwait(false);

        public virtual async Task<Nature> GetNatureAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<Nature>(nameOrId).ConfigureAwait(false);

        public virtual async Task<Stat> GetStatAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<Stat>(nameOrId).ConfigureAwait(false);

        public virtual async Task<Pokemon> GetPokemonAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<Pokemon>(nameOrId).ConfigureAwait(false);

        public virtual async Task<PokemonForm> GetPokemonFormAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<PokemonForm>(nameOrId).ConfigureAwait(false);

        public virtual async Task<PokemonColor> GetPokemonColorAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<PokemonColor>(nameOrId).ConfigureAwait(false);

        public virtual async Task<PokemonShape> GetPokemonShapeAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<PokemonShape>(nameOrId).ConfigureAwait(false);

        public virtual async Task<PokemonSpecies> GetPokemonSpeciesAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<PokemonSpecies>(nameOrId).ConfigureAwait(false);

        public virtual async Task<PokemonHabitat> GetPokemonHabitatAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<PokemonHabitat>(nameOrId).ConfigureAwait(false);

        public virtual async Task<GrowthRate> GetGrowthRateAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<GrowthRate>(nameOrId).ConfigureAwait(false);

        public virtual async Task<Type> GetTypeAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<Type>(nameOrId).ConfigureAwait(false);

        public virtual async Task<PokeathlonStat> GetPokeathlonStatAsync(string nameOrId, CancellationToken cancellationToken = default) => await this.pokeApiClient.GetResourceFromParamAsync<PokeathlonStat>(nameOrId).ConfigureAwait(false);
    }
}
