namespace PokeGraphQL.GraphQL.Resources
{
    using HotChocolate.Types;
    using PokeAPI;
    using PokeGraphQL.GraphQL.Resources.Games;

    internal sealed class GenerationGameIndexType : ObjectType<GenerationGameIndex>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<GenerationGameIndex> descriptor)
        {
            descriptor.FixStructType();
            descriptor.Field(x => x.GameIndex)
                .Description("The internal id of an api resource within game data.");
            descriptor.Field(x => x.Generation)
                .Type<GenerationType>()
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetGenerationAsync(ctx.Parent<GenerationGameIndex>().Generation.Name, token));
        }
    }
}
