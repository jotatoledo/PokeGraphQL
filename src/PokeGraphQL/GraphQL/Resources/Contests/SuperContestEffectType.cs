// <copyright file="SuperContestEffectType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Contests
{
    using HotChocolate.Types;
    using PokeAPI;

    internal sealed class SuperContestEffectType : BaseApiObjectType<SuperContestEffect>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<SuperContestEffect> descriptor)
        {
            // TODO add missing props: flavor_text_entries, moves
            // TODO remove next
            descriptor.BindFields(BindingBehavior.Explicit);
            descriptor.Description("Super contest effects refer to the effects of moves when used in super contests.");
            descriptor.Field(x => x.Appeal)
                .Description("The level of appeal this super contest effect has.");
        }
    }
}
