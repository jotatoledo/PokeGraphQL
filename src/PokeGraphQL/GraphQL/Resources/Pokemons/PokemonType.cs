namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class PokemonType : BaseNamedApiObjectType<Pokemon>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Pokemon> descriptor)
        {
            // TODO implement ignored fields
            descriptor.Description(@"Pokémon are the creatures that inhabit the world of the pokemon games. 
                They can be caught using pokéballs and trained by battling with other pokémon.");
            descriptor.Ignore(x => x.Sprites);
            descriptor.Field(x => x.BaseExperience)
                .Description("The base experience gained for defeating this pokémon.");
            descriptor.Field(x => x.Height)
                .Description("The height of this pokémon.");
            descriptor.Field(x => x.IsDefault)
                .Description("Set for exactly one pokémon used as the default for each species.");
            descriptor.Field(x => x.Order)
                .Description("Order for sorting. Almost national order, except families are grouped together.");
            descriptor.Field(x => x.Mass)
                .Name("weight")
                .Description("The mass of this pokémon.");
            descriptor.Field(x => x.Abilities)
                .Description("A list of abilities this pokémon could potentially have.")
                .Type<ListType<PokemonAbilityType>>();
            descriptor.Field(x => x.Forms)
                .Description("A list of forms this pokémon can take on.")
                .Type<ListType<PokemonFormType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<PokemonResolver>();
                    var resourceTasks = ctx.Parent<Pokemon>()
                        .Forms
                        .Select(form => resolver.GetPokemonFormAsync(form.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
            descriptor.Field(x => x.GameIndices)
                .Description("A list of game indices relevent to pokémon item by generation.")
                .Ignore();
            descriptor.Field(x => x.HeldItems)
                .Description("A list of items this pokémon may be holding when encountered.")
                .Ignore();
            descriptor.Field(x => x.LocationAreaEncounters)
                .Description("A list of location areas as well as encounter details pertaining to specific versions.")
                .Ignore();
            descriptor.Field(x => x.Moves)
                .Description("A list of moves along with learn methods and level details pertaining to specific version groups.")
                .Ignore();
            descriptor.Field(x => x.Species)
                .Description("The species this pokémon belongs to.")
                .Type<PokemonSpeciesType>()
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetPokemonSpeciesAsync(ctx.Parent<Pokemon>().Species.Name, token));
            descriptor.Field(x => x.Stats)
                .Description("A list of base stat values for this pokémon.")
                .Ignore();
            descriptor.Field(x => x.Types)
                .Description("A list of details showing types this pokémon has.")
                .Type<ListType<PokemonTypeMapType>>();
        }

        private sealed class PokemonAbilityType : ObjectType<PokemonAbility>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonAbility> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.IsHidden)
                    .Description("Whether or not this is a hidden ability.");
                descriptor.Field(x => x.Slot)
                    .Description("The slot this ability occupies in this pokémon species.");
                descriptor.Field(x => x.Ability)
                    .Description("The ability the pokémon may have.")
                    .Type<AbilityType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetAbilityAsync(ctx.Parent<PokemonAbility>().Ability.Name, token));
            }
        }

        private sealed class PokemonTypeMapType : ObjectType<PokemonTypeMap>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonTypeMap> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.Slot)
                    .Description("The order the pokémon types are listed in.");
                descriptor.Field(x => x.Type)
                    .Description("The type the referenced pokémon has.")
                    .Type<TypePropertyType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetTypeAsync(ctx.Parent<PokemonTypeMap>().Type.Name, token));
            }
        }
    }
}
