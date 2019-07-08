namespace PokeGraphQL.GraphQL.Resources.Encounters
{
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class EncounterMethodType : BaseNamedApiObjectType<EncounterMethod>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<EncounterMethod> descriptor)
        {
            descriptor.Description("Methods by which the player might can encounter pokémon in the wild, e.g., walking in tall grass.");
            descriptor.Field(x => x.Order)
                .Description("A good value for sorting.");
        }
    }
}
