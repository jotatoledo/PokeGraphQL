// <copyright file="BerryFirmnessType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Berries
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class BerryFirmnessType : BaseNamedApiObjectType<BerryFirmness>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<BerryFirmness> descriptor)
        {
            descriptor.Field(x => x.Berries)
                .Type<ListType<BerryType>>()
                .Resolver((ctx, token) =>
                {
                    var service = ctx.Service<BerryResolver>();
                    var resourceTasks = ctx.Parent<BerryFirmness>()
                        .Berries
                        .Select(berry => service.GetBerryAsync(berry.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
        }
    }
}
