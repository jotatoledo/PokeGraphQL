// <copyright file="ObjectDescriptorExtensions.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL
{
    using System;
    using HotChocolate.Types;

    internal static class ObjectDescriptorExtensions
    {
        [Obsolete("Remove once hotchocolate@9.1.0 is installed")]
        internal static void FixStructType<TClass>(this IObjectTypeDescriptor<TClass> descriptor)
        {
            descriptor.BindFields(BindingBehavior.Explicit);
        }
    }
}
