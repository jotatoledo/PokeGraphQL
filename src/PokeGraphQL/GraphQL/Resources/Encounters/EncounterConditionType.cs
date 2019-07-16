// <copyright file="EncounterConditionType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Encounters
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal sealed class EncounterConditionType : BaseNamedApiObjectType<EncounterCondition>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<EncounterCondition> descriptor)
        {
            descriptor.Description("Conditions which affect what pokémon might appear in the wild, e.g., day or night.");
            descriptor.UseNamedApiResourceCollectionField<EncounterCondition, EncounterConditionValue, EncounterConditionValueType>(x => x.Values);
        }
    }
}
