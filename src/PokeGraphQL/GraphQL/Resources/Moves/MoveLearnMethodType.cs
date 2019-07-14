// <copyright file="MoveLearnMethodType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Moves
{
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Games;

    internal sealed class MoveLearnMethodType : BaseNamedApiObjectType<MoveLearnMethod>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<MoveLearnMethod> descriptor)
        {
            descriptor.Description("Methods by which pokémon can learn moves.");
            descriptor.Ignore(x => x.Descriptions);
            descriptor.Field(x => x.VersionGroups)
                .Description("A list of version groups where moves can be learned through this method.")
                .Type<ListType<VersionGroupType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<GameResolver>();
                    var resourceTasks = ctx.Parent<MoveLearnMethod>()
                        .VersionGroups
                        .Select(versionGroup => resolver.GetVersionGroupAsync(versionGroup.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
        }
    }
}
