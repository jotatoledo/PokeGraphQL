// <copyright file="BerryFirmnessType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Berries
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal sealed class BerryFirmnessType : BaseNamedApiObjectType<BerryFirmness>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<BerryFirmness> descriptor)
        {
            descriptor.UseNamedApiResourceCollectionField<BerryFirmness, Berry, BerryType>(x => x.Berries);
        }
    }
}
