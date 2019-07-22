// <copyright file="ObjectTypeDescriptorExtensions.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL
{
    using HotChocolate.Types;
    using HotChocolate.Types.Relay;
    using PokeApiNet.Data;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources;

    internal static class ObjectTypeDescriptorRootResourceExtensions
    {
        internal static IObjectFieldDescriptor UseApiResource<TResourceType, TSchemaType>(this IObjectFieldDescriptor descriptor)
            where TResourceType : ApiResource
            where TSchemaType : ObjectType<TResourceType> =>
            descriptor.Type<TSchemaType>()
                .Argument("id", a => a.Type<NonNullType<IntType>>().Description("The identifier for the resource."))
                .Resolver((ctx, token) => ctx.Service<PokeApiClient>().GetResourceAsync<TResourceType>(ctx.Argument<int>("id"), token));

        internal static IObjectFieldDescriptor UseNamedApiResource<TResourceType, TSchemaType>(this IObjectFieldDescriptor descriptor)
            where TResourceType : NamedApiResource
            where TSchemaType : ObjectType<TResourceType> =>
            descriptor.Type<TSchemaType>()
                .Argument("nameOrId", a => a.Type<NonNullType<StringType>>().Description("The identifier or name for the resource."))
                .Resolver((ctx, token) => ctx.Service<PokeApiClient>().GetResourceFromParamAsync<TResourceType>(ctx.Argument<string>("nameOrId"), token));

        internal static IObjectFieldDescriptor UseNamedResourcePaging<TResourceType, TSchemaType>(this IObjectFieldDescriptor descriptor)
            where TResourceType : NamedApiResource
            where TSchemaType : ObjectType<TResourceType> =>
            descriptor.AddSimplePaginationArgs()
                .Type<ConnectionWithCountType<TSchemaType>>()
                .Resolver((ctx, token) => ctx.Service<ListResolver>().GetNamedItemsAsync<TResourceType>(
                            ctx.GetCursorProperties(),
                            int.TryParse(ctx.Argument<string>("first"), out var limit) ? limit : default(int?),
                            token));

        internal static IObjectFieldDescriptor UseApiResourcePaging<TResourceType, TSchemaType>(this IObjectFieldDescriptor descriptor)
            where TResourceType : ApiResource
            where TSchemaType : ObjectType<TResourceType> =>
            descriptor.AddSimplePaginationArgs()
                .Type<ConnectionWithCountType<TSchemaType>>()
                .Resolver((ctx, token) => ctx.Service<ListResolver>().GetApiItemsAsync<TResourceType>(
                            ctx.GetCursorProperties(),
                            int.TryParse(ctx.Argument<string>("first"), out var limit) ? limit : default(int?),
                            token));

        internal static IObjectFieldDescriptor AddSimplePaginationArgs(this IObjectFieldDescriptor descriptor) =>
            descriptor
                .Argument("first", a => a.Type<PaginationAmountType>())
                .Argument("after", a => a.Type<StringType>());
    }
}
