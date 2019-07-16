// <copyright file="ContestTypeType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Contests
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Berries;

    internal sealed class ContestTypeType : BaseApiObjectType<ContestType>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<ContestType> descriptor)
        {
            // TODO: create issue about overwritten class properties bounded implicitely
            descriptor.BindFields(BindingBehavior.Explicit);
            descriptor.Description("Contest types are categories judges used to weigh a pokémon's condition in pokemon contests.");
            descriptor.Field(x => x.Name)
                .Description($"The name for this {this.ResourceName} resource.")
                .Type<NonNullType<StringType>>();
            descriptor.UseNamedApiResourceField<ContestType, BerryFlavor, BerryFlavorType>(x => x.BerryFlavor);
        }
    }
}
