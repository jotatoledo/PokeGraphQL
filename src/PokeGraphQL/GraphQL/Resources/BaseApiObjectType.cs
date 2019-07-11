// <copyright file="BaseApiObjectType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources
{
    using System.Text.RegularExpressions;
    using HotChocolate.Types;
    using PokeAPI;

    internal abstract class BaseApiObjectType<TType> : ObjectType<TType>
        where TType : ApiObject
    {
        private static readonly string Pattern = "(.+?)([A-Z])";

        public BaseApiObjectType()
        {
            this.ResourceName = this.CamelCaseSplit(typeof(TType).Name);
        }

        protected string ResourceName { get; set; }

        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<TType> descriptor)
        {
            descriptor.Field(x => x.ID)
                .Name("id")
                .Description($"The identifier for this {this.ResourceName} resource");
            this.ConcreteConfigure(descriptor);
        }

        protected abstract void ConcreteConfigure(IObjectTypeDescriptor<TType> descriptor);

        private string CamelCaseSplit(string source)
        {
            return Regex.Matches(source, Pattern).Count == 0
                ? source.ToLower()
                : Regex.Replace(source, Pattern, Eval);
        }

        private static string Eval(Match match) => $"{match.Groups[1].Value.ToLower()} {match.Groups[2].Value.ToLower()}";
    }
}
