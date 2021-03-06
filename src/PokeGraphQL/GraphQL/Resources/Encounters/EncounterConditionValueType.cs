﻿// <copyright file="EncounterConditionValueType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Encounters
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal sealed class EncounterConditionValueType : BaseNamedApiObjectType<EncounterConditionValue>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<EncounterConditionValue> descriptor)
        {
            descriptor.Description("Encounter condition values are the various states that an encounter condition can have, i.e., Time of day can be either day or night.");
            descriptor.UseNamedApiResourceField<EncounterConditionValue, EncounterCondition, EncounterConditionType>(x => x.Condition);
        }
    }
}
