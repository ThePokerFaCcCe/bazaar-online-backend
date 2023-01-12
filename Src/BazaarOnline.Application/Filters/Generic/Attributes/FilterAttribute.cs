namespace BazaarOnline.Application.Filters.Generic.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class FilterAttribute : Attribute

    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterType">Type of filtering condition that should be used</param>
        public FilterAttribute(FilterTypeEnum filterType)
        {
            FilterType = filterType;
        }

        /// <summary>
        /// Type of filtering condition that should be used
        /// </summary>
        public FilterTypeEnum FilterType { get; set; }

        /// <summary>
        /// Property name that should be used for this filter.
        /// Default value is the Filter Property name
        /// </summary>
        public string? ModelPropertyName { get; set; }

        // /// <summary>
        // /// Filter result Regardless of uppercase or lowercase
        // /// </summary>
        // public bool IgnoreCase { get; set; } = false;
    }
}
