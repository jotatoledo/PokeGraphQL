namespace PokeGraphQL.GraphQL.Resources.Contests
{
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class ContestEffectType : BaseApiObjectType<ContestEffect>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<ContestEffect> descriptor)
        {
            // TODO add missing props: effect_entries, flavor_text_entries
            // TODO remove next
            descriptor.BindFields(BindingBehavior.Explicit);
            descriptor.Description("Contest effects refer to the effects of moves when used in contests.");
            descriptor.Field(x => x.Appeal)
                .Description("The base number of hearts the user of this move gets.");
            descriptor.Field(x => x.Jam)
                .Description("The base number of hearts the user's opponent loses.");
        }
    }
}
