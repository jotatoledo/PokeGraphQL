// <copyright file="MoveDamageClassType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Moves
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class MoveDamageClassType : BaseNamedApiObjectType<MoveDamageClass>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<MoveDamageClass> descriptor)
        {
            descriptor.Description("Damage classes moves can have, e.g. physical, special, or status (non-damaging).");
            descriptor.Ignore(x => x.Descriptions);
            descriptor.Field(x => x.Moves)
                .Description("A list of moves that fall into this damage class.")
                .Type<ListType<MoveType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<MoveResolver>();
                    var resourceTasks = ctx.Parent<MoveDamageClass>()
                        .Moves
                        .Select(move => resolver.GetMoveAsync(move.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
        }
    }
}
