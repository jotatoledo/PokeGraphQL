namespace PokeGraphQL.GraphQL.Resources.Moves
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;
    using PokeGraphQL.GraphQL.Resources.Games;

    internal sealed class MoveLearnMethodType : BaseNamedApiObjectType<MoveLearnMethod>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<MoveLearnMethod> descriptor)
        {
            descriptor.Description("Methods by which pokémon can learn moves.");
            descriptor.Ignore(x => x.Descriptions);
            descriptor.Field(x => x.VersionGroups)
                .Description("A list of version groups where moves can be learned through this method.")
                .Type<ListType<VersionGroupType>>()
                .Resolver(async (ctx, token) =>
                {
                    var resolver = ctx.Service<GameResolver>();
                    var resourceTasks = ctx.Parent<MoveLearnMethod>()
                        .VersionGroups
                        .Select(versionGroup => resolver.GetVersionGroupAsync(versionGroup.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
        }
    }
}
