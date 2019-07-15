// <copyright file="BaseNamedApiObjectType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources
{
    using HotChocolate.Types;
    using PokeApiNet.Models;

    internal abstract class BaseNamedApiObjectType<TType> : BaseApiObjectType<TType>
        where TType : NamedApiResource
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<TType> descriptor)
        {
            descriptor.Field(x => x.Name)
                .Description($"The name for this {this.ResourceName} resource.")
                .Type<NonNullType<StringType>>();

            // TODO rework into something less sensitive; renaming of the `Names` prop or API changes will break this
            if (typeof(TType).GetProperty("Names") != null)
            {
                descriptor.Field("Names").Ignore();
            }

            base.Configure(descriptor);
        }
    }
}
