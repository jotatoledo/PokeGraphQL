// <copyright file="MoveAilmentType.cs" company="PokeGraphQL.Net">
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

    internal sealed class MoveAilmentType : BaseNamedApiObjectType<MoveAilment>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<MoveAilment> descriptor)
        {
            descriptor.Description("Move Ailments are status conditions caused by moves used during battle.");
            descriptor.Field(x => x.Moves)
                .Description("A list of moves that cause this ailment.")
                .Type<ListType<MoveType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<MoveResolver>();
                    var resourceTasks = ctx.Parent<MoveAilment>()
                        .Moves
                        .Select(move => resolver.GetMoveAsync(move.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
        }
    }
}
