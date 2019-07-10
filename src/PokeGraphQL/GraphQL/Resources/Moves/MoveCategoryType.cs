namespace PokeGraphQL.GraphQL.Resources.Moves
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class MoveCategoryType : BaseNamedApiObjectType<MoveCategory>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<MoveCategory> descriptor)
        {
            descriptor.Description("Very general categories that loosely group move effects.");
            descriptor.Ignore(x => x.Descriptions);
            descriptor.Field(x => x.Moves)
                .Description("A list of moves that fall into this category.")
                .Type<ListType<MoveType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<MoveResolver>();
                    var resourceTasks = ctx.Parent<MoveCategory>()
                        .Moves
                        .Select(move => resolver.GetMoveAsync(move.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
        }
    }
}
