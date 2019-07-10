﻿namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class EggGroupType : BaseNamedApiObjectType<EggGroup>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<EggGroup> descriptor)
        {
            descriptor.Description(@"Egg Groups are categories which determine which Pokémon are able to interbreed. 
                Pokémon may belong to either one or two Egg Groups.");
            descriptor.Field(x => x.Species)
                .Description("A list of all pokémon species that are members of this egg group.")
                .Type<ListType<PokemonSpeciesType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<PokemonResolver>();
                    var resourceTasks = ctx.Parent<EggGroup>()
                        .Species
                        .Select(species => resolver.GetPokemonSpeciesAsync(species.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
        }
    }
}
