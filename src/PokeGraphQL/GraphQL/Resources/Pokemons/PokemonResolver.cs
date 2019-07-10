namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using PokeAPI;
    using TypeProperty = PokeAPI.PokemonType;

    public class PokemonResolver
    {
        public virtual async Task<Ability> GetAbilityAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<Ability>(nameOrId).ConfigureAwait(false);

        public virtual async Task<Characteristic> GetCharacteristicAsync(int id, CancellationToken cancellationToken = default) => await DataFetcher.GetApiObject<Characteristic>(id).ConfigureAwait(false);

        public virtual async Task<EggGroup> GetEggGroupAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<EggGroup>(nameOrId).ConfigureAwait(false);

        public virtual async Task<Gender> GetGenderAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<Gender>(nameOrId).ConfigureAwait(false);

        public virtual async Task<Nature> GetNatureAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<Nature>(nameOrId).ConfigureAwait(false);

        public virtual async Task<Stat> GetStatAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<Stat>(nameOrId).ConfigureAwait(false);

        public virtual async Task<Pokemon> GetPokemonAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<Pokemon>(nameOrId).ConfigureAwait(false);

        public virtual async Task<PokemonForm> GetPokemonFormAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<PokemonForm>(nameOrId).ConfigureAwait(false);

        public virtual async Task<PokemonColour> GetPokemonColorAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<PokemonColour>(nameOrId).ConfigureAwait(false);

        public virtual async Task<PokemonShape> GetPokemonShapeAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<PokemonShape>(nameOrId).ConfigureAwait(false);

        public virtual async Task<PokemonSpecies> GetPokemonSpeciesAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<PokemonSpecies>(nameOrId).ConfigureAwait(false);

        public virtual async Task<PokemonHabitat> GetPokemonHabitatAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<PokemonHabitat>(nameOrId).ConfigureAwait(false);

        public virtual async Task<GrowthRate> GetGrowthRateAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<GrowthRate>(nameOrId).ConfigureAwait(false);

        public virtual async Task<TypeProperty> GetTypeAsync(string nameOrId, CancellationToken cancellationToken = default) => await DataFetcher.GetNamedApiObject<TypeProperty>(nameOrId).ConfigureAwait(false);
    }
}
