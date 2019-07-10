﻿namespace PokeGraphQL.GraphQL.Resources.Moves
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class MoveTargetType : BaseNamedApiObjectType<MoveTarget>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<MoveTarget> descriptor)
        {
            descriptor.Description("Targets moves can be directed at during battle. Targets can be pokémon, environments or even other moves.");
            descriptor.Ignore(x => x.Descriptions);
            descriptor.Field(x => x.Moves)
                .Description("A list of moves that that are directed at this target.")
                .Type<ListType<MoveType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<MoveResolver>();
                    var resourceTasks = ctx.Parent<MoveTarget>()
                        .Moves
                        .Select(move => resolver.GetMoveAsync(move.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
        }
    }
}
