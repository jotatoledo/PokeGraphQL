namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class CharacteristicType : BaseApiObjectType<Characteristic>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<Characteristic> descriptor)
        {
            descriptor.Description(@"Characteristics indicate which stat contains a Pokémon's highest IV.
                A Pokémon's Characteristic is determined by the remainder of its highest IV divided by 5 (gene_modulo).");
            descriptor.Field(x => x.GeneModulo)
                .Description("The remainder of the highest stat/IV divided by 5.");
            descriptor.Field(x => x.PossibleValues)
                .Description("The possible values of the highest stat that would result in a pokémon recieving this characteristic when divided by 5.")
                .Type<ListType<IntType>>();
            descriptor.Field(x => x.HighestStat)
                .Description("The highest stat of this characteristic")
                .Type<StatType>()
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetStatAsync(ctx.Parent<Characteristic>().HighestStat.Name, token));
            descriptor.Ignore(x => x.Descriptions);
        }
    }
}
