// <copyright file="MachineResolver.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Machines
{
    using System.Threading;
    using System.Threading.Tasks;
    using PokeAPI;

    public class MachineResolver
    {
        public virtual async Task<Machine> GetMachineAsync(int id, CancellationToken cancellationToken = default) => await DataFetcher.GetApiObject<Machine>(id).ConfigureAwait(false);
    }
}
