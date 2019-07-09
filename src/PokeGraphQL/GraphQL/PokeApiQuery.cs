namespace PokeGraphQL.GraphQL
{
    using HotChocolate.Types;
    using PokeGraphQL.GraphQL.Resources.Berries;
    using PokeGraphQL.GraphQL.Resources.Contests;
    using PokeGraphQL.GraphQL.Resources.Encounters;
    using PokeGraphQL.GraphQL.Resources.Games;
    using PokeGraphQL.GraphQL.Resources.Items;
    using PokeGraphQL.GraphQL.Resources.Locations;

    public sealed class PokeApiQuery : ObjectType
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name("Query")
                .Description("All the Pokémon data you'll ever need in one place, easily accessible through a modern GraphQL API.");

            descriptor.Field("berry")
                .Type<BerryType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<BerryResolver>().GetBerryAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("berryFirmness")
                .Type<BerryFirmnessType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<BerryResolver>().GetBerryFirmnessAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("berryFlavor")
                .Type<BerryFlavorType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<BerryResolver>().GetBerryFlavorAsync(ctx.Argument<string>("nameOrId"), token));

            descriptor.Field("item")
                .Type<ItemType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<ItemResolver>().GetItemAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("itemAttribute")
                .Type<ItemAttributeType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<ItemResolver>().GetAttributeAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("itemCategory")
                .Type<ItemCategoryType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<ItemResolver>().GetCategoryAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("itemPocket")
                .Type<ItemPocketType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<ItemResolver>().GetPocketAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("itemFlingEffect")
                .Type<ItemFlingEffectType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<ItemResolver>().GetFlingEffectAsync(ctx.Argument<string>("nameOrId"), token));

            descriptor.Field("contestType")
                .Type<ContestTypeType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<ContestResolver>().GetContestTypeAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("contestEffect")
                .Type<ContestEffectType>()
                .Argument("id", a => a.Type<IntType>().Description("The identifier for the resource."))
                .Resolver((ctx, token) => ctx.Service<ContestResolver>().GetContestEffectAsync(ctx.Argument<int>("id"), token));
            descriptor.Field("superContestEffect")
                .Type<SuperContestEffectType>()
                .Argument("id", a => a.Type<IntType>().Description("The identifier for the resource."))
                .Resolver((ctx, token) => ctx.Service<ContestResolver>().GetSuperContestEffectAsync(ctx.Argument<int>("id"), token));

            descriptor.Field("encounterMethod")
                .Type<EncounterMethodType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<EncounterResolver>().GetEncounterMethodAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("encounterCondition")
                .Type<EncounterConditionType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<EncounterResolver>().GetEncounterConditionAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("encounterConditionValue")
                .Type<EncounterConditionValueType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<EncounterResolver>().GetEncounterConditionValueAsync(ctx.Argument<string>("nameOrId"), token));

            descriptor.Field("generation")
                .Type<GenerationType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetGenerationAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("pokedex")
                .Type<PokedexType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetPokedexAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("version")
                .Type<VersionType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetVersionAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("versionGroup")
                .Type<VersionGroupType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetVersionGroupAsync(ctx.Argument<string>("nameOrId"), token));

            descriptor.Field("location")
                .Type<LocationType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<LocationResolver>().GetLocationAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("locationArea")
                .Type<LocationAreaType>()
                .Argument("id", a => a.Type<IntType>().Description("The identifier for the resource."))
                .Resolver((ctx, token) => ctx.Service<LocationResolver>().GetLocationAreaAsync(ctx.Argument<int>("id"), token));
            descriptor.Field("palParkArea")
                .Type<PalParkAreaType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<LocationResolver>().GetPalParkAreaAsync(ctx.Argument<string>("nameOrId"), token));
            descriptor.Field("region")
                .Type<RegionType>()
                .Argument("nameOrId", a => a.Type<StringType>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<LocationResolver>().GetRegionAsync(ctx.Argument<string>("nameOrId"), token));
        }
    }
}
