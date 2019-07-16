// <copyright file="ObjectTypeDescriptorNestedResourceExtensions.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using DotNetFunctional.Maybe;
    using HotChocolate.Types;
    using PokeApiNet.Data;
    using PokeApiNet.Models;

    internal static class ObjectTypeDescriptorNestedResourceExtensions
    {
        internal static IObjectFieldDescriptor UseNullableNamedApiResourceField<TSource, TTarget, TSchemaType>(
            this IObjectTypeDescriptor<TSource> descriptor,
            Expression<Func<TSource, NamedApiResource<TTarget>>> fieldSelector)
            where TTarget : NamedApiResource
            where TSchemaType : ObjectType<TTarget> =>
            descriptor.Field(fieldSelector)
                .Type<TSchemaType>()
                .Resolver(ctx => Maybe.Lift(ctx.Parent<TSource>())
                    .Select(fieldSelector.Compile())
                    .Match(ctx.Service<PokeApiClient>().GetResourceAsync, Task.FromResult<TTarget>(default)));

        internal static IObjectFieldDescriptor UseNamedApiResourceCollectionField<TSource, TTarget, TSchemaType>(
            this IObjectTypeDescriptor<TSource> descriptor,
            Expression<Func<TSource, IEnumerable<NamedApiResource<TTarget>>>> fieldSelector)
            where TTarget : NamedApiResource
            where TSchemaType : ObjectType<TTarget> =>
            descriptor.Field(fieldSelector)
                .Type<ListType<TSchemaType>>()
                .Resolver((ctx, token) => ctx.Service<PokeApiClient>().GetResourceAsync(fieldSelector.Compile()(ctx.Parent<TSource>())));

        internal static IObjectFieldDescriptor UseNamedApiResourceField<TSource, TTarget, TSchemaType>(
            this IObjectTypeDescriptor<TSource> descriptor,
            Expression<Func<TSource, NamedApiResource<TTarget>>> fieldSelector)
            where TTarget : NamedApiResource
            where TSchemaType : ObjectType<TTarget> =>
            descriptor.Field(fieldSelector)
                .Type<NonNullType<TSchemaType>>()
                .Resolver((ctx, token) => ctx.Service<PokeApiClient>().GetResourceAsync(fieldSelector.Compile()(ctx.Parent<TSource>())));

        internal static IObjectFieldDescriptor UseNullableApiResourceField<TSource, TTarget, TSchemaType>(
            this IObjectTypeDescriptor<TSource> descriptor,
            Expression<Func<TSource, ApiResource<TTarget>>> fieldSelector)
            where TTarget : ApiResource
            where TSchemaType : ObjectType<TTarget> =>
            descriptor.Field(fieldSelector)
                .Type<TSchemaType>()
                .Resolver(ctx => Maybe.Lift(ctx.Parent<TSource>())
                    .Select(fieldSelector.Compile())
                    .Match(ctx.Service<PokeApiClient>().GetResourceAsync, Task.FromResult<TTarget>(default)));

        internal static IObjectFieldDescriptor UseApiResourceCollectionField<TSource, TTarget, TSchemaType>(
            this IObjectTypeDescriptor<TSource> descriptor,
            Expression<Func<TSource, IEnumerable<ApiResource<TTarget>>>> fieldSelector)
            where TTarget : ApiResource
            where TSchemaType : ObjectType<TTarget> =>
            descriptor.Field(fieldSelector)
                .Type<ListType<TSchemaType>>()
                .Resolver((ctx, token) => ctx.Service<PokeApiClient>().GetResourceAsync(fieldSelector.Compile()(ctx.Parent<TSource>())));

        internal static IObjectFieldDescriptor UseApiResourceField<TSource, TTarget, TSchemaType>(
            this IObjectTypeDescriptor<TSource> descriptor,
            Expression<Func<TSource, ApiResource<TTarget>>> fieldSelector)
            where TTarget : ApiResource
            where TSchemaType : ObjectType<TTarget> =>
            descriptor.Field(fieldSelector)
                .Type<NonNullType<TSchemaType>>()
                .Resolver((ctx, token) => ctx.Service<PokeApiClient>().GetResourceAsync(fieldSelector.Compile()(ctx.Parent<TSource>())));
    }
}
