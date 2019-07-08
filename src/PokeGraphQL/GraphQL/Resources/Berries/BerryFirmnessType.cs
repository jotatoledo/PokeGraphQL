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
                .Resolver(async (ctx, token) =>
                {
                    var service = ctx.Service<BerryResolver>();
                    var resourceTasks = ctx.Parent<BerryFirmness>().Berries.Select(berry => service.GetBerryAsync(berry.Name, token));
                    return await Task.WhenAll(resourceTasks);
                });
        }
    }
}
