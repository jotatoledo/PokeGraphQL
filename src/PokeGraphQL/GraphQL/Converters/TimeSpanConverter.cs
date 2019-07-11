// <copyright file="TimeSpanConverter.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Converters
{
    using System;
    using HotChocolate.Utilities;

    internal sealed class TimeSpanConverter : ITypeConverter
    {
        /// <inheritdoc/>
        public Type From => typeof(TimeSpan);

        /// <inheritdoc/>
        public Type To => typeof(int);

        /// <inheritdoc/>
        public object Convert(object source)
        {
            if (source is TimeSpan timeSpan)
            {
                return System.Convert.ToInt32(timeSpan.TotalHours);
            }
            else
            {
                throw new ArgumentException(nameof(source));
            }
        }
    }
}
