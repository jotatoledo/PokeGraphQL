// <copyright file="GenerationGameIndexType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Common
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Games;

    internal sealed class GenerationGameIndexType : ObjectType<GenerationGameIndex>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<GenerationGameIndex> descriptor)
        {
            descriptor.Field(x => x.GameIndex)
                .Description("The internal id of an api resource within game data.");
            descriptor.Field(x => x.Generation)
                .Type<GenerationType>()
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetGenerationAsync(ctx.Parent<GenerationGameIndex>().Generation.Name, token));
        }
    }
}
