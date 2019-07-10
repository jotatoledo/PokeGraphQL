﻿namespace PokeGraphQL.GraphQL.Resources.Encounters
{
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class EncounterConditionValueType : BaseNamedApiObjectType<EncounterConditionValue>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<EncounterConditionValue> descriptor)
        {
            descriptor.Description("Encounter condition values are the various states that an encounter condition can have, i.e., Time of day can be either day or night.");
            descriptor.Field(x => x.Condition)
                .Description("The condition this encounter condition value pertains to.")
                .Type<EncounterConditionType>()
                .Resolver((ctx, token) => ctx.Service<EncounterResolver>().GetEncounterConditionAsync(ctx.Parent<EncounterConditionValue>().Condition.Name, token));
        }
    }
}