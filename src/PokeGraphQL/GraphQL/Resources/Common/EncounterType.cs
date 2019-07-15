// <copyright file="EncounterType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Common
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Encounters;

    internal sealed class EncounterType : ObjectType<Encounter>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<Encounter> descriptor)
        {
            descriptor.Field(x => x.ConditionValues)
                .Type<ListType<EncounterConditionValueType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<EncounterResolver>();
                    var resourceTasks = ctx.Parent<Encounter>()
                        .ConditionValues
                        .Select(conditionValue => resolver.GetEncounterConditionValueAsync(conditionValue.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
            descriptor.Field(x => x.Method)
                .Type<EncounterMethodType>()
                .Resolver((ctx, token) => ctx.Service<EncounterResolver>().GetEncounterMethodAsync(ctx.Parent<Encounter>().Method.Name, token));

            // TODO remove once hotchocolate@9.1.0 lands
            descriptor.Field(x => x.MinLevel);
            descriptor.Field(x => x.MaxLevel);
            descriptor.Field(x => x.Chance);
        }
    }
}
