namespace PokeGraphQL.GraphQL.Resources
{
    using HotChocolate.Types;
    using PokeAPI;

    internal abstract class BaseNamedApiObjectType<TType> : BaseApiObjectType<TType>
        where TType : NamedApiObject
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<TType> descriptor)
        {
            descriptor.Field(x => x.Name)
                .Description($"The name for this {this.ResourceName} resource.")
                .Type<NonNullType<StringType>>();
            descriptor.Ignore(x => x.Names);
            base.Configure(descriptor);
        }
    }
}
