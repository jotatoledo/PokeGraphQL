namespace PokeGraphQL.GraphQL.Resources.Moves
{
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class MoveBattleStyleType : BaseNamedApiObjectType<MoveBattleStyle>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<MoveBattleStyle> descriptor)
        {
            descriptor.Description("Styles of moves when used in the Battle Palace.");
        }
    }
}
