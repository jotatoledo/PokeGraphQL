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
