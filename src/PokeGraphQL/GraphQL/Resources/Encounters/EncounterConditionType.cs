namespace PokeGraphQL.GraphQL.Resources.Encounters
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class EncounterConditionType : BaseNamedApiObjectType<EncounterCondition>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<EncounterCondition> descriptor)
        {
            descriptor.Description("Conditions which affect what pokémon might appear in the wild, e.g., day or night.");
            descriptor.Field(x => x.Values)
                .Description("A list of possible values for this encounter condition.")
                .Type<ListType<EncounterConditionValueType>>()
                .Resolver(async (ctx, token) =>
                {
                    var service = ctx.Service<EncounterResolver>();
                    var resourceTasks = ctx.Parent<EncounterCondition>()
                    .Values
                    .Select(value => service.GetEncounterConditionValueAsync(value.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
        }
    }
}
