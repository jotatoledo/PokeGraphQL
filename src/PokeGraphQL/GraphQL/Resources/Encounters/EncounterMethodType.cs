// <copyright file="EncounterMethodType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Encounters
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

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
