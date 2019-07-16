// <copyright file="EncounterType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Common
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Encounters;

    internal sealed class EncounterType : ObjectType<Encounter>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<Encounter> descriptor)
        {
            descriptor.UseNamedApiResourceCollectionField<Encounter, EncounterConditionValue, EncounterConditionValueType>(x => x.ConditionValues);
            descriptor.UseNamedApiResourceField<Encounter, EncounterMethod, EncounterMethodType>(x => x.Method);
        }
    }
}
