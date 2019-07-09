namespace PokeGraphQL.GraphQL.Resources.Locations
{
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class PalParkAreaType : BaseNamedApiObjectType<PalParkArea>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<PalParkArea> descriptor)
        {
            // TODO implement ignored fields
            descriptor.Description("Areas used for grouping pokémon encounters in Pal Park. They're like habitats that are specific to Pal Park.");
            descriptor.Field(x => x.Encounters)
                .Description("A list of pokémon encountered in thi pal park area along with details")
                .Type<ListType<PalParkEncounterSpeciesType>>();
        }

        private sealed class PalParkEncounterSpeciesType : ObjectType<PalParkEncounterSpecies>
        {
            protected override void Configure(IObjectTypeDescriptor<PalParkEncounterSpecies> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.BaseScore)
                    .Description("The base score given to the player when this pokémon is caught during a pal park run.");
                descriptor.Field(x => x.Rate)
                    .Description("The base rate for encountering this pokémon in this pal park area.");

                // TODO implement ignored fields
                descriptor.Field(x => x.Species)
                    .Description("The pokémon species being encountered.")
                    .Ignore();
            }
        }
    }
}
